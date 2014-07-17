using System.Configuration;

namespace EdFiValidation.ApiProxy.Core.Utility
{
    public interface IConfig
    {
        int SessionIdSegmentIndex { get; }
        int DestinationUrlSegementIndex { get; }
    }

    public class Config : IConfig
    {
        public int SessionIdSegmentIndex
        {
            get
            {
                int segmentIndex;
                int.TryParse(ConfigurationManager.AppSettings["SessionIdSegementIndex"], out segmentIndex); // static ref (abstract it?)
                return segmentIndex;
            }
        }
        public int DestinationUrlSegementIndex
        {
            get
            {
                int segmentIndex;
                int.TryParse(ConfigurationManager.AppSettings["DestinationUrlSegementIndex"], out segmentIndex); // static ref (abstract it?)
                return segmentIndex;
            }
        }
    }
}
