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

        private readonly MongoDatabase _db;
        private readonly IDataList<UseCase> _useCases;

        public UseCaseUpdateTask(IConfig config, IDataList<UseCase> useCases)
        {
            var url = new MongoUrl(config.ProxyDbConnectionString);
            _db = new MongoClient(url).GetServer().GetDatabase(url.DatabaseName);

            _useCases = useCases;
        }


        public void Execute(Action<string> postAction)
        {
            var collection = _db.GetCollection<UseCase>();
            _useCases.GetList().ToList().ForEach(uc =>
            {
                var query = Query<UseCase>.EQ(e => e.Id, uc.Id);
                if (collection.Find(query).FirstOrDefault() == null)
                {
                    collection.Save(uc);
                }
            });
        }
    }
}
