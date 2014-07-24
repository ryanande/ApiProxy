using System;
using System.Linq;
using Database.Data;
using EdFiValidation.ApiProxy.Core.Models;
using MongoDB.Driver;

namespace Database.Create
{
    public class UseCaseCreateTask : ICreateTask
    {
        private readonly MongoCollection<UseCase> _collection;
        private readonly IDataList<UseCase> _useCases;
        // this could be read from a json/ xml source
        // all default 
       

        public UseCaseCreateTask(IConfig config, IDataList<UseCase> useCases)
        {
            var url = new MongoUrl(config.ProxyDbConnectionString);
            var db = new MongoClient(url)
                .GetServer()
                .GetDatabase(url.DatabaseName);

            _collection = db.GetCollection<UseCase>("UseCase");

            _useCases = useCases;
        }

        public void Execute(Action<string> action)
        {
            _useCases.GetList().ToList().ForEach(u => _collection.Save(u));

            if (action != null)
                action.Invoke("UseCaseCreateTask Complete.");
        }


    }
}
