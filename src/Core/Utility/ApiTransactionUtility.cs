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
            if (_config.SessionIdSegmentIndex < 0)
                throw new InvalidConfigurationValueException("Invalid SessionIdSegmentIndex value in ApiTransactionUtility._config: {0}", _config.SessionIdSegmentIndex);
            
            return uri.Segments.Count() - 1 < _config.SessionIdSegmentIndex
                 ? string.Empty : uri.Segments[_config.SessionIdSegmentIndex].Replace("/", "");
        }


        public string ExtractDestination(Uri uri)
        {
            if (_config.DestinationUrlSegementIndex < 0)
                throw new InvalidConfigurationValueException("Invalid DestinationUrlSegementIndex value in ApiTransactionUtility._config: {0}", _config.DestinationUrlSegementIndex);

            var dest = uri.Segments[_config.DestinationUrlSegementIndex].Replace("/", "");
            dest = WebUtility.UrlDecode(dest);

            return DecodeDestination(dest);
        }


        public string DecodeDestination(string encodedUrl)
        {
            string decodedUrl;
            try
            {
                byte[] data = Convert.FromBase64String(encodedUrl);
                decodedUrl = Encoding.UTF8.GetString(data);
            }
            catch (FormatException ex)
            {
                throw new CannotParseUriException("Error while trying to decode destination url from Base-64 format. " + ex.Message);
            }
           
            return decodedUrl;
        }


        public Uri BuildDestinationUri(Uri uri)
        {
            if (uri.Segments.Count() - 1 < _config.DestinationUrlSegementIndex)
            {
                throw new CannotParseUriException(
                    "Error parsing URI. Not enough URI segments. {0} detected. At least {1} required. " + CannotParseUriException.ExpectedFormatMsg,
                    uri.Segments.Count(), _config.DestinationUrlSegementIndex + 1);
            }

            // the 4 represents the segment used for the final destination endpoint
            string destinationPath = uri.Segments.Skip(4).Aggregate((m, n) => m + n);
            // decode url, should be fourth segment in the incoming uri
            var destinationRoot = ExtractDestination(uri);

            UriBuilder destinationUri;
            try
            {
                 destinationUri = new UriBuilder(destinationRoot)
                {
                    Path = destinationPath
                };
            }
            catch (UriFormatException ex)
            {
                throw new CannotParseUriException("Decoded destination uri was invalid. " + ex.Message);
            }
            
            if (!string.IsNullOrWhiteSpace(uri.Query))
                destinationUri.Query = uri.Query.Replace("?", "");

            return destinationUri.Uri;
        }
    }
}