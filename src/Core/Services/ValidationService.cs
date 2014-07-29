using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EdFiValidation.ApiProxy.Core.Commands;
using EdFiValidation.ApiProxy.Core.Handlers;
using EdFiValidation.ApiProxy.Core.Models;
using EdFiValidation.ApiProxy.Core.Queries;
using EdFiValidation.ApiProxy.Core.Utility;

namespace EdFiValidation.ApiProxy.Core.Services
{
    public class ValidationService : IValidationService
    {
        private readonly IRequestResponsePairQueryService _requestResponsePairQueryService;
        private readonly IUseCaseQueryService _useCaseQueryService;
        private readonly ICommandHandler<CreateUseCaseValidation> _commandHandler;

        public ValidationService(IRequestResponsePairQueryService requestResponsePairQueryService,
                                    IUseCaseQueryService useCaseQueryService,
                                    ICommandHandler<CreateUseCaseValidation> commandHandler)
        {
            _requestResponsePairQueryService = requestResponsePairQueryService;
            _useCaseQueryService = useCaseQueryService;
            _commandHandler = commandHandler;
        }
        public IEnumerable<UseCase> Validate(string sessionId)
        {
            var requestResponses = _requestResponsePairQueryService.GetOnSessionId(sessionId);
            var useCases = _useCaseQueryService.GetAll();


            // this is a very niaeve stab at the mathing rule.
            var passedUseCases = (from useCase in useCases
                                  let correctCounter = useCase.Items.Count(item => 
                                      requestResponses.Any(r => 
                                          string.Equals(r.ApiRequest.UriAccessed, item.Path, StringComparison.CurrentCultureIgnoreCase) && // path and
                                          string.Equals(r.ApiRequest.HttpMethod, item.Method, StringComparison.CurrentCultureIgnoreCase))) // method
                                  where correctCounter == useCase.Items.Count
                                  select useCase).ToList();


            // persist the successes for the session (log em).
            if (passedUseCases.Count > 0)
                _commandHandler.Handle(new CreateUseCaseValidation
                {
                    Id = CombGuid.Generate(),
                    ClientId = Guid.Empty,
                    SessionId = sessionId,
                    Cases = passedUseCases
                });

            return passedUseCases;
        }
    }
}