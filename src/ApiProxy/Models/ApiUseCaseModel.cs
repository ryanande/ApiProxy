using System;
using System.Collections.Generic;

namespace EdFiValidation.ApiProxy.Models
{
    public class ApiUseCaseModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsOrdered { get; set; }

        public List<ApiUseCaseItemModel> Items { get; set; }
    }

    public class ApiUseCaseItemModel
    {
        public Guid Id { get; set; }
        public string Path { get; set; }
        public string Method { get; set; }
    }
}