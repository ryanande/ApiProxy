using System.Configuration;

namespace Database
{
    public interface IConfig
    {
        string ProxyDbConnectionString { get; }
    }

    public class Config : IConfig
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