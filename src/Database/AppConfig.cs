using System.Configuration;

namespace Database
{
    public class AppConfig : IAppConfig
    {
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