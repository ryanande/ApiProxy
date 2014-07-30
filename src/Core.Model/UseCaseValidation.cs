using System;
using System.Collections.Generic;

namespace EdFiValidation.ApiProxy.Core.Models
{

    public class Validation : ModelBase
    {

        public Guid ClientId { get; set; }
        public string SessionId { get; set; }
        public DateTime ValidationDate { get; set; }
        public List<ValidationUseCase> UseCases { get; set; }  
        public override string ToString()
        {
            return string.Format("[{0}] - {1}", ValidationDate, SessionId);
        }
    }

    public class ValidationUseCase : ModelBase
    {
        public string Title { get; set; }
        public List<ValidationUseCaseItem> Items { get; set; } 
        
        public override string ToString()
        {
            return Title;
        }
    }

    public class ValidationUseCaseItem : ModelBase
    {
        public string Path { get; set; }
        public string Method { get; set; }
        
        public override string ToString()
        {
            return Method + ": " + Path;
        }
    }
}
