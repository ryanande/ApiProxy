using System;
using System.Linq;
using Database.Data;
using EdFiValidation.ApiProxy.Core.Models;
using EdFiValidation.ApiProxy.Utilities;
using MongoDB.Driver;

namespace Database.Create
{
    public class UseCaseCreateTask : ICreateTask
    {
        private readonly MongoDatabase _db;
        private readonly IDataList<UseCase> _useCases;
        // this could be read from a json/ xml source
        // all default 
       

        public UseCaseCreateTask(IAppConfig appConfig, IDataList<UseCase> useCases)
        {
            var url = new MongoUrl(appConfig.ProxyDbConnectionString);
            _db = new MongoClient(url)
                .GetServer()
                .GetDatabase(url.DatabaseName);


            _useCases = useCases;
        }

        public void Execute(Action<string> action)
        {
            var collection = _db.GetCollection<UseCase>();

            _useCases.GetList().ToList().ForEach(u => collection.Save(u));

            if (action != null)
                action.Invoke("UseCaseCreateTask Complete.");
        }


    }
}
