using System;

namespace EdFiValidation.ApiProxy.Core.Models
{
    public class UseCaseValidation : ModelBase
    {
        public Guid ClientId { get; set; }
        public Guid UseCaseId { get; set; }
        public string SessionId { get; set; }
        public DateTime ValidationDate { get; set; }
        public double Paths { get; set; }


        public override string ToString()
        {
            return string.Format("[{0}] - {1}", ValidationDate, SessionId);
        }
    }
}
