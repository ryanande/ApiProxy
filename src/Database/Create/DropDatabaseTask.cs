using System;
using MongoDB.Driver;

namespace Database.Create
{
    public class DropDatabaseTask : ITask
    {
        private readonly MongoDatabase _mongoDatabase;
        public DropDatabaseTask(IConfig config)
        {
            var url = new MongoUrl(config.ProxyDbConnectionString);
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
