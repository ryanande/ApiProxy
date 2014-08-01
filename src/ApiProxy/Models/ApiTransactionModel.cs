using System;
using System.Collections.Generic;

namespace EdFiValidation.ApiProxy.Models
{
    public class ApiTransactionModel : UiModelBase
    {
        public string UriAccessed { get; set; }
        public string BodyContent { get; set; }
        public Dictionary<string, string> Headers { get; set; }

        public string RequestPath()
        {
            return string.IsNullOrWhiteSpace(UriAccessed) ?
                string.Empty :
                new Uri(UriAccessed).PathAndQuery;
        }
    }
}