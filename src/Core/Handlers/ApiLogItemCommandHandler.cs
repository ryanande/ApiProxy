using System;
using EdFiValidation.ApiProxy.Core.Commands;
using EdFiValidation.ApiProxy.Core.Models;
using EdFiValidation.ApiProxy.Core.Utility;
using MongoDB.Driver;

namespace EdFiValidation.ApiProxy.Core.Handlers
{
    public class ApiLogItemCommandHandler : ICommandHandler<CreateApiLogItem>
    {
        private readonly MongoCollection<RequestResponsePair> _collection;

        public ApiLogItemCommandHandler(IConfig config)
        {
            var url = new MongoUrl(config.ProxyDbConnectionString);
            var db = new MongoClient(url)
                .GetServer()
                .GetDatabase(url.DatabaseName);

            _collection = db.GetCollection<RequestResponsePair>("RequestResponsePair");
        }
        public void Handle(CreateApiLogItem command)
        {
            _collection.Insert(new RequestResponsePair
            {
                Id = CombGuid.Generate(),
                SessionId = command.SessionId,
                LogDate = DateTime.Now,
                ApiRequest = command.ApiRequest,
                ApiResponse = command.ApiResponse
            });
        }
    }
}
