
namespace EdFiValidation.ApiProxy.Core.Handlers
{
    public interface ICommandHandler<in T>
    {
        void Handle(T command);
    }
}
