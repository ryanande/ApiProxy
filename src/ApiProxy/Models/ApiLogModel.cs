using System;

namespace EdFiValidation.ApiProxy.Models
{
    public class ApiLogModel
    {
        public Guid Id { get; set; }
        public DateTime LogDate { get; set; }
        public string SessionId { get; set; }
        public ApiRequestModel ApiRequest { get; set; }
        public ApiResponseModel ApiResponse { get; set; }
    }
}