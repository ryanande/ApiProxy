namespace EdFiValidation.ApiProxy.Models
{
    public class ApiResponseModel : ApiTransactionModel
    {
        public int ResponseStatusCode { get; set; }
        public string ResponseStatusMessage { get; set; }
        public bool IsSuccessStatusCode { get; set; }
        public string ReasonPhrase { get; set; }

    }
}