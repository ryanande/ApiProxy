
namespace EdFiValidation.ApiProxy.Core.Models
{
    public class UseCaseItem : ModelBase
    {
        public string Path { get; set; }
        public string Method { get; set; }


        public override string ToString()
        {
            return string.Format("({0}) {1}", Method, Path);
        }
    }
}