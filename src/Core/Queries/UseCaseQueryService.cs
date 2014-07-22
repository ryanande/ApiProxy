using EdFiValidation.ApiProxy.Core.Models;
using EdFiValidation.ApiProxy.Core.Utility;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EdFiValidation.ApiProxy.Core.Queries
{
    public class UseCaseQueryService : IUseCaseQueryService
    {
        private readonly MongoCollection<UseCase> _collection;
        public UseCaseQueryService(IConfig config)
        {
            var url = MongoUrl.Create(config.ProxyDbConnectionString);
            var db = new MongoClient(url)
                .GetServer()
                .GetDatabase(url.DatabaseName);

            _collection = db.GetCollection<UseCase>("RequestResponsePair");
        }


        public IEnumerable<UseCase> GetOnId(Guid useCaseId)
        {
            return _collection.AsQueryable().Where(l => l.Id == useCaseId); // this should expand to include paging and possibly filtering
        }

    }
}