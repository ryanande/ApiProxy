using System;

namespace EdFiValidation.ApiProxy.Core.Models
{
    public class RequestResponsePair : ModelBase
    {
        public DateTime LogDate { get; set; }
        public string SessionId { get; set; }
        public ApiRequest ApiRequest { get; set; }
        public ApiResponse ApiResponse { get; set; }
    }
}
