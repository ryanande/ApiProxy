using System;
using MongoDB.Bson.Serialization.Attributes;

namespace EdFiValidation.ApiProxy.Core.Models
{
    public abstract class ModelBase
    {
        [BsonId]
        public Guid Id { get; set; }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
