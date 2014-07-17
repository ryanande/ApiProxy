using System.Collections.Generic;
using EdFiValidation.ApiProxy.Core.Models;

namespace EdFiValidation.ApiProxy.Core.Queries
{
    public interface IApiLogItemQueryService
    {
        IEnumerable<RequestResponsePair> GetSessionItems(string sessionId);
    }
}