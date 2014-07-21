﻿using System.Net;
using EdFiValidation.ApiProxy.Core.Commands;
using EdFiValidation.ApiProxy.Core.Handlers;
using EdFiValidation.ApiProxy.Core.Utility;
using System.Net.Http;

namespace EdFiValidation.ApiProxy.Core.Services
{
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

            // HttpClient and WebRequest objs both bark when GET w/ body content
            if (request.Method.Method.ToUpper() == "GET")
                request.Content = null;

            // reset the request URI to the decoded path
            var uri = _apiTransactionUtility.BuildDestinationUri(request.RequestUri);
            request.RequestUri = uri;

            using (var client = new HttpClient())
            {
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

                var response = client.SendAsync(request).Result;
                apiLog.ApiResponse = _apiTransactionUtility.BuildApiResponse(response);

                // why we have to reset this? (comes in as localhost if not)
                apiLog.ApiResponse.UriAccessed = uri.OriginalString;

                _commandHandler.Handle(apiLog); // async could be nice

                return response;
            }
        }
    }
}