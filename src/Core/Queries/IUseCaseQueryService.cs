using System;
using System.Collections.Generic;
using EdFiValidation.ApiProxy.Core.Models;

namespace EdFiValidation.ApiProxy.Core.Queries
{
    public interface IUseCaseQueryService
    {
        IEnumerable<UseCase> GetOnId(Guid useCaseId);
        IEnumerable<UseCase> GetAll();
    }
}
