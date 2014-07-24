namespace EdFiValidation.ApiProxy.Core.Services
{
    public interface IValidationService
    {
        // vNext
        //bool ValidateSession(Guid clientId, string sessionId);
        void ValidateSession(string sessionId);
    }
}
