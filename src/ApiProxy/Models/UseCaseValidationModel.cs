using System;
using System.Collections.Generic;

namespace EdFiValidation.ApiProxy.Models
{

    public class ValidationCaseModel : UiModelBase
    {
        public Guid UseCaseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<ValidationCaseItemModel> Items { get; set; } 

    }

    public class ValidationCaseItemModel : UiModelBase
    {
        public string Path { get; set; }
        public string Method { get; set; }
        public bool IsPassed { get; set; }
    }
}