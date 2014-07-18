using System;

using MongoDB.Bson.Serialization.Attributes;

namespace EdFiValidation.ApiProxy.Core.Models
{
    public class RequestResponsePair
    {
        [BsonId]
        public Guid Id { get; set; }
        public DateTime LogDate { get; set; }
        public string SessionId { get; set; }
        public ApiRequest ApiRequest { get; set; }
        public ApiResponse ApiResponse { get; set; }
    }
}
