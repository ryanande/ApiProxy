using System.Collections.Generic;
using EdFiValidation.ApiProxy.Core.Models;

namespace EdFiValidation.ApiProxy.Core.Queries
{
    public interface IRequestResponsePairQueryService
    {
        IEnumerable<RequestResponsePair> GetOnSessionId(string sessionId);
    }
}