using System.Net.Http;

namespace EdFiValidation.ApiProxy.Core.Services
{
    public interface IApiRequestProcessor
    {
        HttpResponseMessage Execute(HttpRequestMessage request, string[] urlSegments);

    }
}