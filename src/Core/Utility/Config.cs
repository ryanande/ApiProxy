using System.Configuration;

namespace EdFiValidation.ApiProxy.Core.Utility
{
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

        public string ProxyDbConnectionString
        {
            get
            {
                var connectionString = ConfigurationManager.ConnectionStrings["ProxyDbConnectionString"];
                if (connectionString == null || string.IsNullOrWhiteSpace(connectionString.ConnectionString))
                    throw new ConfigurationErrorsException("ProxyDbConnectionString is required in the configuration.");

                return connectionString.ConnectionString;
            }
        }
    }
}