using System;
using System.Collections.Generic;
using System.Net.Http;
using EdFiValidation.ApiProxy.Core.Models;

namespace EdFiValidation.ApiProxy.Core.Utility
{
    public interface IApiTransactionUtility
    {
        ApiResponse BuildApiResponse(HttpResponseMessage response);
        ApiRequest BuildApiRequest(HttpRequestMessage request);
        string ExtractBody(HttpContent content);
        Dictionary<string, string> ExtractHeaders(List<KeyValuePair<string, IEnumerable<string>>> headers);
        string ExtractSessionId(Uri uri);
        string ExtractDestination(Uri uri);
        string DecodeDestination(string encodedUrl);
        Uri BuildDestinationUri(Uri uri);
    }
}