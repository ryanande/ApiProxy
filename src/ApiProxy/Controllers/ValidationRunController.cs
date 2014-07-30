using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using EdFiValidation.ApiProxy.Core.Commands;
using EdFiValidation.ApiProxy.Core.Handlers;
using EdFiValidation.ApiProxy.Core.Models;
using EdFiValidation.ApiProxy.Core.Queries;
using EdFiValidation.ApiProxy.Models;
using EdFiValidation.ApiProxy.Core.Services;

namespace EdFiValidation.ApiProxy.Controllers
{
    public class ValidationRunController : ApiController
    {
        private readonly IRequestResponsePairQueryService _requestResponseRepo;
        private readonly IUseCaseQueryService _useCaseRepo;
        private readonly ICommandHandler<CreateUseCaseValidation> _commandHandler; //me thinks the runtime is choking on this. Am I missing a concrete factory? Abstract Class? 

        public ValidationRunController(IRequestResponsePairQueryService pairs, IUseCaseQueryService useCaseQueryService, ICommandHandler<CreateUseCaseValidation> commandHandler)
        {
            _requestResponseRepo = pairs;
            _useCaseRepo = useCaseQueryService;
            _commandHandler = commandHandler;
        }

        [Route("~/ValidateRun/{id}")]
        [HttpGet] 
        public ApiValidationIndexModel Index(string id)
        {
            var logEntries = _requestResponseRepo.GetOnSessionId(id).ToList();
 
            var model = new ApiValidationIndexModel
                {
                    SessionId = id,
                    TotalRequests = logEntries.Count(),
                    RequestErrors = logEntries.Count(l => l.ApiResponse.IsSuccessStatusCode == false)
                };
            return model;
        }

        [Route("~/ExecuteValidation/{id}")]
        [HttpGet]
        public List<UseCaseValidationModel> ExecuteValidation(string id)
        {
            var validationService = new ValidationService(_requestResponseRepo, _useCaseRepo, _commandHandler);

            var passedUseCases = validationService.Validate(id).ToList();
            var model = new List<UseCaseValidationModel>();

            foreach (var passedUseCase in passedUseCases) //this loops smells in at least two funny ways
            {
                var passedUseCaseItemModels = passedUseCase.Items.Select(useCaseItem => new ApiUseCaseItemModel
                    {
                        Id = useCaseItem.Id, Method = useCaseItem.Method, Path = useCaseItem.Path
                    }).ToList();

                var passedUseCaseModel = new UseCaseValidationModel
                {
                    UseCaseId = passedUseCase.Id,
                    Title = passedUseCase.Title, 
                    Description = passedUseCase.Description,
                    Items = passedUseCaseItemModels
                };

                model.Add(passedUseCaseModel);
            }

        return model;
        }

        //-- Execute Validation
        //List<UseCaseValidationModel>
        //{
        //  UseCaseId: "guid",
        //  Title: "",
        //  Description: "",
        //  Items: List<UseCaseItemModel>		
        //}

    }
}
