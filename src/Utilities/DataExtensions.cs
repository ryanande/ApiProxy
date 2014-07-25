using MongoDB.Driver;

namespace EdFiValidation.ApiProxy.Utilities
{
    public static class DataExtensions
    {
        public static MongoCollection<T> GetCollection<T>(this MongoDatabase db)
        {
            var tableName = typeof(T).Name;
            return db.GetCollection<T>(tableName);
        }
    }
}
