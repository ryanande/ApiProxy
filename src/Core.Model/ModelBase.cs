using System;
using MongoDB.Bson.Serialization.Attributes;

namespace EdFiValidation.ApiProxy.Core.Models
{
    public class ModelBase
    {
        [BsonId]
        public Guid Id { get; set; }
    }
}
