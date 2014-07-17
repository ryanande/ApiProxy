using EdFiValidation.ApiProxy.Core.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace EdFiValidation.ApiProxy.Core.Queries
{
    public class ApiLogItemQueryService : IApiLogItemQueryService
    {
        private readonly MongoCollection<RequestResponsePair> _collection;
        public ApiLogItemQueryService()
        {
            var url = GetConnectionString("ProxyDbConnectionString");
            var db = new MongoClient(url)
                .GetServer()
                .GetDatabase(url.DatabaseName);

            _collection = db.GetCollection<RequestResponsePair>("RequestResponsePair");
        }


        public IEnumerable<RequestResponsePair> GetSessionItems(string sessionId)
        {
            return _collection.AsQueryable().Where(l => l.SessionId == sessionId); // this should expand to include paging and possibly filtering
        }


        private static MongoUrl GetConnectionString(string connectionStringName)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[connectionStringName];
            return MongoUrl.Create(connectionString != null ? connectionString.ConnectionString : connectionStringName);
        }
    }
}
