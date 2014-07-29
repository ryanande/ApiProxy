using System;
using MongoDB.Driver;

namespace Database.Tasks
{
    public class DropDatabaseTask : ITask
    {
        private readonly MongoDatabase _mongoDatabase;
        public DropDatabaseTask(IAppConfig appConfig)
        {
            var url = new MongoUrl(appConfig.ProxyDbConnectionString);
            _mongoDatabase = new MongoClient(url)
                .GetServer()
                .GetDatabase(url.DatabaseName);
        }
        public void Execute(Action<string> postAction)
        {
            _mongoDatabase.Drop();
            if(postAction != null)
                postAction.Invoke("Database dropped successfully.");
        }
    }
}
