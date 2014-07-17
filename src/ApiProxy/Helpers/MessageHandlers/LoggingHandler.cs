using System;
using EdFiValidation.ApiProxy.Core.Commands;
using EdFiValidation.ApiProxy.Core.Handlers;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EdFiValidation.ApiProxy.Helpers.MessageHandlers
{
    [Obsolete]
    public class LoggingHandler : DelegatingHandler
    {
        private readonly ICommandHandler<CreateApiLogItem> _commandHandler;
        public LoggingHandler(ICommandHandler<CreateApiLogItem> commandHandler)
        {
            _commandHandler = commandHandler;
        }


        public LoggingHandler(HttpMessageHandler innerHandler, ICommandHandler<CreateApiLogItem> commandHandler)
            : base(innerHandler)
        {
            _commandHandler = commandHandler;
        }


        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Log the request information
            LogRequestLoggingInfo(request);
            var sessionId = ExtractSessionId(request.RequestUri);
            // Execute the request
            return base.SendAsync(request, cancellationToken).ContinueWith(task =>
            {
                var response = task.Result;
                // Extract the response logging info then persist the information
                LogResponseLoggingInfo(response, sessionId);
                return response;
            });
        }


        private void LogRequestLoggingInfo(HttpRequestMessage request)
        {
            var sessionId = ExtractSessionId(request.RequestUri);

            var info = new CreateApiLogItem
            {
                SessionId = sessionId,
                MessageType = HttpMessageType.Request,
                HttpMethod = request.Method.Method,
                UriAccessed = request.RequestUri.AbsoluteUri,
                IpAddress = HttpContext.Current != null ? HttpContext.Current.Request.UserHostAddress : "0.0.0.0",
                Headers = ExtractHeaders(request.Headers.ToList()),

                BodyContent = ExtractBody(request.Content)
            };

            _commandHandler.Handle(info);
        }


        private void LogResponseLoggingInfo(HttpResponseMessage response, string sessionId)
        {
            var info = new CreateApiLogItem
            {
                SessionId = sessionId,
                MessageType = HttpMessageType.Response,
                HttpMethod = response.RequestMessage.Method.ToString(),
                UriAccessed = response.RequestMessage.RequestUri.AbsoluteUri,
                IpAddress = HttpContext.Current != null ? HttpContext.Current.Request.UserHostAddress : "0.0.0.0",
                Headers = ExtractHeaders(response.Headers.ToList()),

                BodyContent = ExtractBody(response.Content),

                ResponseStatusCode = response.StatusCode,
                ResponseStatusMessage = response.ReasonPhrase
            };

            _commandHandler.Handle(info);
        }


        private static string ExtractBody(HttpContent content)
        {
            if (content == null)
            {
                return string.Empty;
            }

            var task = content.ReadAsByteArrayAsync();
            return Encoding.UTF8.GetString(task.Result);
        }


        private static List<string> ExtractHeaders(List<KeyValuePair<string, IEnumerable<string>>> headers)
        {
            var headerItems = new List<string>();
            headers.ForEach(h =>
            {
                // convert the header values into one long string from a series of IEnumerable<string> values so it looks for like a HTTP header
                var headerValues = new StringBuilder();

                if (h.Value != null)
                {
                    foreach (var hv in h.Value)
                    {
                        if (headerValues.Length > 0)
                        {
                            headerValues.Append(", ");
                        }
                        headerValues.Append(hv);
                    }
                }
                headerItems.Add(string.Format("{0}: {1}", h.Key, headerValues.ToString()));
            });
            return headerItems;
        }

        private string ExtractSessionId(Uri uri)
        {
            return uri.Segments[2].Replace("/","");
        }
    }
}
