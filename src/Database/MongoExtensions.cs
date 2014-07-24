using MongoDB.Driver;

namespace Database
{
    public static class MongoExtensions
    {
        public static MongoCollection<T> GetCollection<T>(this MongoDatabase db)
        {
            var tableName = typeof(T).Name;
            return db.GetCollection<T>(tableName);
        }
    }
}
