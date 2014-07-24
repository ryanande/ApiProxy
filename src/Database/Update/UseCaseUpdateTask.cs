using Database.Data;
using EdFiValidation.ApiProxy.Core.Models;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Linq;

namespace Database.Update
{
    public class UseCaseUpdateTask : IUpdateTask
    {

        private readonly MongoCollection<UseCase> _collection;
        private readonly IDataList<UseCase> _useCases;

        public UseCaseUpdateTask(IConfig config, IDataList<UseCase> useCases)
        {
            var url = new MongoUrl(config.ProxyDbConnectionString);
            var db = new MongoClient(url)
                .GetServer()
                .GetDatabase(url.DatabaseName);

            _collection = db.GetCollection<UseCase>("UseCase");
            _useCases = useCases;
        }


        public void Execute(Action<string> postAction)
        {
            _useCases.GetList().ToList().ForEach(uc =>
            {
                var query = Query<UseCase>.EQ(e => e.Id, uc.Id);
                if (_collection.Find(query).FirstOrDefault() == null)
                {
                    _collection.Save(uc);
                }
            });
        }
    }
}
