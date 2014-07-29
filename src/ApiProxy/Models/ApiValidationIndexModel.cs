using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EdFiValidation.ApiProxy.Models
{
    public class ApiValidationIndexModel
    {
        public string SessionId { get; set; }
        public int TotalRequests { get; set; }
        public int RequestErrors { get; set; }
        public List<ApiLogModel> LogEntries { get; set; }
    }
}