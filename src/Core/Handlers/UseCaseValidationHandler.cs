using System;
using System.Linq;
using EdFiValidation.ApiProxy.Core.Commands;
using EdFiValidation.ApiProxy.Core.Models;
using EdFiValidation.ApiProxy.Core.Utility;
using EdFiValidation.ApiProxy.Utilities;
using MongoDB.Driver;

namespace EdFiValidation.ApiProxy.Core.Handlers
{
    public class UseCaseValidationHandler : ICommandHandler<CreateUseCaseValidation>
    {
        private readonly MongoDatabase _db;

        public UseCaseValidationHandler(IConfig config)
        {
            var url = new MongoUrl(config.ProxyDbConnectionString);
            _db = new MongoClient(url)
                .GetServer()
                .GetDatabase(url.DatabaseName);

            
        }

        public void Handle(CreateUseCaseValidation command)
        {
            var useCaseValidation = new Validation
            {
                Id = command.Id,
                ClientId = command.ClientId,
                SessionId = command.SessionId,
                ValidationDate = DateTime.Now,
                UseCases = command.Cases.Select(u => new ValidationUseCase
                {
                    Id = u.Id,
                    Title = u.Title,
                    Items = u.Items.Select(i => new ValidationUseCaseItem
                    {
                        Id = i.Id,
                        Path = i.Path,
                        Method = i.Method
                    }).ToList()
                }).ToList()
            };

            var collection = _db.GetCollection<ValidationUseCase>();
            collection.Save(useCaseValidation);
        }
    }
}
