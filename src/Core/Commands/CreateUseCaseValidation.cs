using System;
using System.Collections.Generic;
using EdFiValidation.ApiProxy.Core.Models;

namespace EdFiValidation.ApiProxy.Core.Commands
{
    public class CreateUseCaseValidation : ICommand
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public string SessionId { get; set; }

        public List<UseCase> Cases { get; set; }
    }
}
