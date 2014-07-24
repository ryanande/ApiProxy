using EdFiValidation.ApiProxy.Core.Commands;
using EdFiValidation.ApiProxy.Core.Models;
using EdFiValidation.ApiProxy.Core.Utility;
using MongoDB.Driver;

namespace EdFiValidation.ApiProxy.Core.Handlers
{
    class UseCaseValidationHandler : ICommandHandler<CreateUseCaseValidation>
    {
        private readonly MongoCollection<UseCaseValidation> _collection;

        public UseCaseValidationHandler(IConfig config)
        {
            var url = new MongoUrl(config.ProxyDbConnectionString);
            var db = new MongoClient(url)
                .GetServer()
                .GetDatabase(url.DatabaseName);

            _collection = db.GetCollection<UseCaseValidation>("UseCaseValidation");
        }

        public void Handle(CreateUseCaseValidation command)
        {
            //TODO: create UseCaseValidation object and save
        }
    }
}
