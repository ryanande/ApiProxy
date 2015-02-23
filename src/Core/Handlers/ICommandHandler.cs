using EdFiValidation.ApiProxy.Core.Commands;

namespace EdFiValidation.ApiProxy.Core.Handlers
{
    public interface ICommandHandler<in T> where T : ICommand
    {
        void Handle(T command);
    }
}
