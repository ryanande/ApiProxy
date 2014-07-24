using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using EdFiValidation.ApiProxy.Core.Models;

namespace EdFiValidation.ApiProxy.Core.Utility
{
    public class ApiTransactionUtility : IApiTransactionUtility
    {
        private readonly IConfig _config;
        public ApiTransactionUtility(IConfig config)
        {
            _config = config;
        }


        public ApiRequest BuildApiRequest(HttpRequestMessage request)
        {
            return new ApiRequest
            {
                HttpMethod = request.Method.Method,
                UriAccessed = request.RequestUri.AbsoluteUri,
                IpAddress = HttpContext.Current != null ? HttpContext.Current.Request.UserHostAddress : "0.0.0.0",
                Headers = ExtractHeaders(request.Headers.ToList()),
                BodyContent = ExtractBody(request.Content)
            };
        }


        public ApiResponse BuildApiResponse(HttpResponseMessage response)
        {
            return new ApiResponse
            {
                UriAccessed = response.RequestMessage.RequestUri.AbsoluteUri,
                Headers = ExtractHeaders(response.Headers.ToList()),
                BodyContent = ExtractBody(response.Content),
                ResponseStatusCode = response.StatusCode,
                ResponseStatusMessage = response.ReasonPhrase
            };
        }


        public string ExtractBody(HttpContent content)
        {
            if (content == null)
            {
                return string.Empty;
            }

            var task = content.ReadAsByteArrayAsync();
            return Encoding.UTF8.GetString(task.Result);
        }


        public Dictionary<string, string> ExtractHeaders(List<KeyValuePair<string, IEnumerable<string>>> headers)
        {
            var headerItems = new Dictionary<string, string>();
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
                headerItems.Add(h.Key, headerValues.ToString());
            });
            return headerItems;
        }


        public string ExtractSessionId(Uri uri)
        {
            return uri.Segments.Count() - 1 < _config.SessionIdSegmentIndex ||
                _config.SessionIdSegmentIndex < 0 ?
                string.Empty :
                uri.Segments[_config.SessionIdSegmentIndex].Replace("/", "");
        }


        public string ExtractDestination(Uri uri)
        {
            if (uri.Segments.Count() - 1 < _config.DestinationUrlSegementIndex || _config.DestinationUrlSegementIndex < 0)
                return null; // we may want to throw a custom exception here...


            var dest = uri.Segments[_config.DestinationUrlSegementIndex].Replace("/", "");
            dest = WebUtility.UrlDecode(dest);

            return DecodeDestination(dest);
        }


        public string DecodeDestination(string encodedUrl)
        {
            byte[] data = Convert.FromBase64String(encodedUrl);
            string decodedUrl = Encoding.UTF8.GetString(data);
            return decodedUrl;
        }


        public Uri BuildDestinationUri(Uri uri)
        {
            string destinationPath;
            try
            {
                // the 4 represents the segment used for the final destination endpoint
                destinationPath = uri.Segments.Skip(4).Aggregate((m, n) => m + n);
            }
            catch (System.InvalidOperationException)
            {
                throw new CannotParseUriException("Not enough URI segments. {0} detected. At least {1} required.",
                                                  uri.Segments.Count(), 5); //this smells. Is there a way we can calculte the required segments? 
            }
            // foo.com/api/{sessionId}/{EncodedDestination}/{clientId}/{DistionationAction}/{id}/...  becomes   {clientId}/{DistionationAction?}/{id}/...

            // decode url, should be fourth segment in the incoming uri

            var rootDestination = ExtractDestination(uri);

            var uriBuilder = new UriBuilder(rootDestination)
                {
                    Path = destinationPath
                };

            if (!string.IsNullOrWhiteSpace(uri.Query))
                uriBuilder.Query = uri.Query.Replace("?", "");

            return uriBuilder.Uri;
        }
    }
}