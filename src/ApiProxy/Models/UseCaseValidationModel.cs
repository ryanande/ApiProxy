using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EdFiValidation.ApiProxy.Models
{
    public class UseCaseValidationModel
    {
        public Guid UseCaseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<ApiUseCaseItemModel> Items { get; set; }
    }
}