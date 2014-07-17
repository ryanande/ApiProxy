using System.Collections.Generic;

namespace EdFiValidation.ApiProxy.Core.Models
{
    public class ApiTransaction
    {
        public string UriAccessed { get; set; }
        public string BodyContent { get; set; }
        public Dictionary<string, string> Headers { get; set; }
    }
}