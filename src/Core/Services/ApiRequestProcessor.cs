using System.Net;
using EdFiValidation.ApiProxy.Core.Commands;
using EdFiValidation.ApiProxy.Core.Handlers;
using EdFiValidation.ApiProxy.Core.Utility;
using System.Net.Http;

namespace EdFiValidation.ApiProxy.Core.Services
{
    public interface IApiRequestProcessor
    {
        HttpResponseMessage Execute(HttpRequestMessage request, string[] urlSegments);

    }


    public class ApiRequestProcessor : IApiRequestProcessor
    {
        private readonly IApiTransactionUtility _apiTransactionUtility;
        private readonly ICommandHandler<CreateApiLogItem> _commandHandler;
        public ApiRequestProcessor(IApiTransactionUtility apiTransactionUtility, ICommandHandler<CreateApiLogItem> commandHandler)
        {
            _apiTransactionUtility = apiTransactionUtility;
            _commandHandler = commandHandler;
        }


        // smell?
        public HttpResponseMessage Execute(HttpRequestMessage request, string[] urlSegments)
        {
            var apiLog = new CreateApiLogItem
            {
                SessionId = _apiTransactionUtility.ExtractSessionId(request.RequestUri),
                ApiRequest = _apiTransactionUtility.BuildApiRequest(request)
            };

            var uri = _apiTransactionUtility.BuildDestinationUri(request.RequestUri);
            request.RequestUri = uri;

            if (request.Method.Method.ToUpper() == "GET")
                request.Content = null;


            using (var client = new HttpClient())
            {
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

                var response = client.SendAsync(request).Result;
                apiLog.ApiResponse = _apiTransactionUtility.BuildApiResponse(response);

                _commandHandler.Handle(apiLog); // async could be nice

                return response;
            }
        }
    }
}