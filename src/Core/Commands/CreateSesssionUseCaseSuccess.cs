using System;

namespace EdFiValidation.ApiProxy.Core.Commands
{
    public class CreateSesssionUseCaseSuccess : ICommand
    {
        public Guid Id { get; set; }
    }
}
