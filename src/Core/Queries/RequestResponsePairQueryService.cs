using EdFiValidation.ApiProxy.Core.Models;
using EdFiValidation.ApiProxy.Core.Utility;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Linq;

namespace EdFiValidation.ApiProxy.Core.Queries
{
    public class RequestResponsePairQueryService : IRequestResponsePairQueryService
    {
        private readonly MongoCollection<RequestResponsePair> _collection;
        public RequestResponsePairQueryService(IConfig config)
        {

            var url = MongoUrl.Create(config.ProxyDbConnectionString);
            var db = new MongoClient(url)
                .GetServer()
                .GetDatabase(url.DatabaseName);

            _collection = db.GetCollection<RequestResponsePair>();
        }


        public IEnumerable<RequestResponsePair> GetOnSessionId(string sessionId)
        {
            return _collection.AsQueryable().Where(l => l.SessionId == sessionId).ToList(); // this should expand to include paging and possibly filtering
        }
    }
}
