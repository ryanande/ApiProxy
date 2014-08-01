using System;
using System.Collections.Generic;

namespace EdFiValidation.ApiProxy.Models
{
    public class ValidationModel : UiModelBase
    {
        
    }
    public class UseCaseValidationModel : UiModelBase
    {
        public Guid UseCaseId { get; set; }
        public bool Passed { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<ApiUseCaseItemModel> Items { get; set; }
    }
}