using System.Collections.Generic;
using EdFiValidation.ApiProxy.Core.Models;

namespace EdFiValidation.ApiProxy.Core.Services
{
    public interface IValidationService
    {
        // vNext
        //bool Validate(Guid clientId, string sessionId);
        IEnumerable<UseCase> Validate(string sessionId);
    }
}
