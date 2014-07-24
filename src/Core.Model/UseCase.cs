using System.Collections.Generic;

namespace EdFiValidation.ApiProxy.Core.Models
{
    public class UseCase : ModelBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsOrdered { get; set; }

        public List<UseCaseItem> Items { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
