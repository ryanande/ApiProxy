using System;
using EdFiValidation.ApiProxy.Core.Commands;
using EdFiValidation.ApiProxy.Core.Models;
using EdFiValidation.ApiProxy.Core.Utility;
using MongoDB.Driver;

namespace EdFiValidation.ApiProxy.Core.Handlers
{
    public class ApiLogItemCommandHandler : ICommandHandler<CreateApiLogItem>
    {
        private readonly MongoDatabase _db;

        public ApiLogItemCommandHandler(IConfig config)
        {
            var url = new MongoUrl(config.ProxyDbConnectionString);
            _db = new MongoClient(url)
                .GetServer()
                .GetDatabase(url.DatabaseName);
        }
        public void Handle(CreateApiLogItem command)
        {
            var requestResponsePair = new RequestResponsePair
            {
                Id = command.Id,
                SessionId = command.SessionId,
                LogDate = DateTime.Now,
                ApiRequest = command.ApiRequest,
                ApiResponse = command.ApiResponse
            };
            

            var collection = _db.GetCollection<RequestResponsePair>();
            collection.Insert(requestResponsePair);
        }
    }


    
}
