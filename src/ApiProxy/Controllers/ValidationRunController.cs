﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using EdFiValidation.ApiProxy.Core.Commands;
using EdFiValidation.ApiProxy.Core.Handlers;
using EdFiValidation.ApiProxy.Core.Queries;
using EdFiValidation.ApiProxy.Models;
using EdFiValidation.ApiProxy.Core.Services;
using AutoMapper;

namespace EdFiValidation.ApiProxy.Controllers
{
    public class ValidationRunController : ApiController
    {
        private readonly IRequestResponsePairQueryService _requestResponseRepo;
        private readonly IUseCaseQueryService _useCaseRepo;
        private readonly ICommandHandler<CreateUseCaseValidation> _commandHandler;
        private readonly IValidationService _validationService;

        public ValidationRunController(IRequestResponsePairQueryService pairs, IUseCaseQueryService useCaseQueryService, ICommandHandler<CreateUseCaseValidation> commandHandler)
        {
            _requestResponseRepo = pairs;
            _useCaseRepo = useCaseQueryService;
            _commandHandler = commandHandler;
            _validationService = new ValidationService(_requestResponseRepo, _useCaseRepo, _commandHandler);
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
            var passedUseCases = _validationService.Validate(id).ToList();
            //var passedUseCases = _useCaseRepo.GetAll().ToList(); //sweet shortcut hack
            var model = new List<UseCaseValidationModel>();

            foreach (var passedUseCase in passedUseCases) //this loops smells less funny than before
            {
                var passedUseCaseItemModels = Mapper.Map<List<ApiUseCaseItemModel>>(passedUseCase.Items);                

                var passedUseCaseModel = Mapper.Map<UseCaseValidationModel>(passedUseCase);
                passedUseCaseModel.Items = passedUseCaseItemModels;

                model.Add(passedUseCaseModel);
            }
        return model;
        }
    }
}
