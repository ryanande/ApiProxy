using System;
using System.Collections.Generic;
using EdFiValidation.ApiProxy.Core.Models;

namespace EdFiValidation.ApiProxy.Models
{
    public class UseCaseValidationModel : ModelBase
    {
        public Guid UseCaseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<ApiUseCaseItemModel> Items { get; set; }
    }
}