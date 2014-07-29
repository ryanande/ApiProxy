namespace EdFiValidation.ApiProxy.Core.Services
{
    public interface IValidationService
    {
        // vNext
        //bool Validate(Guid clientId, string sessionId);
        void Validate(string sessionId);
    }
}
