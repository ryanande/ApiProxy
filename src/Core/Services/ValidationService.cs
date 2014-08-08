using System;
using System.Collections.Generic;
using System.Globalization;
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
        public IEnumerable<ValidationUseCase> Validate(string sessionId)
        {
            var requestResponses = _requestResponsePairQueryService.GetOnSessionId(sessionId);
            var useCases = _useCaseQueryService.GetAll();


            // this is the lameist code I have ever written


            var validationUseCases = from uc in useCases
                                     select new ValidationUseCase
                                     {
                                         Id = uc.Id,
                                         Title = uc.Title,
                                         Description = uc.Description,
                                         Items = uc.Items.Select(i => new ValidationUseCaseItem
                                         {
                                             Id = i.Id,
                                             Path = i.Path,
                                             Method = i.Method,
                                             IsPassed = requestResponses.Any(rr => string.Equals(new Uri(rr.ApiResponse.UriAccessed).AbsolutePath, i.Path, StringComparison.CurrentCultureIgnoreCase) &&
                                             string.Equals(rr.ApiRequest.HttpMethod, i.Method, StringComparison.CurrentCultureIgnoreCase) &&
                                             ((int)rr.ApiResponse.ResponseStatusCode).ToString(CultureInfo.InvariantCulture).StartsWith("20"))
                                         }).ToList()
                                     };


            var enumerable = validationUseCases as ValidationUseCase[] ?? validationUseCases.ToArray();
            var passed = enumerable.Where(c => c.Items.Count == c.Items.Count(i => i.IsPassed)).Select(v =>
                new UseCase
                    {
                        Id = v.Id,
                        Title = v.Title,
                        Description = v.Description,
                        Items = v.Items.Select(vi => new UseCaseItem
                        {
                            Id = vi.Id,
                            Method = vi.Method,
                            Path = vi.Path
                        }).ToList()
                    }
                );


            _commandHandler.Handle(new CreateUseCaseValidation
            {
                Id = CombGuid.Generate(),
                ClientId = Guid.Empty,
                SessionId = sessionId,
                Cases = passed.ToList()
            });


            return enumerable;
        }
    }
}