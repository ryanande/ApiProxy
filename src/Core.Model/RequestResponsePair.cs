using System;

namespace EdFiValidation.ApiProxy.Core.Models
{
    public class RequestResponsePair : ModelBase
    {
        public DateTime LogDate { get; set; }
        public string SessionId { get; set; }
        public ApiRequest ApiRequest { get; set; }
        public ApiResponse ApiResponse { get; set; }

        public override string ToString()
        {
            return string.Format("{0}: [{1}]", SessionId, LogDate);
        }
    }
}
