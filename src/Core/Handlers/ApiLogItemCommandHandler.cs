using System;
using System.Configuration;
using EdFiValidation.ApiProxy.Core.Commands;
using EdFiValidation.ApiProxy.Core.Models;
using MongoDB.Driver;

namespace EdFiValidation.ApiProxy.Core.Handlers
{
    public class ApiLogItemCommandHandler : ICommandHandler<CreateApiLogItem>
    {
        private readonly MongoCollection<RequestResponsePair> _collection;

        public ApiLogItemCommandHandler()
        {
            var url = GetConnectionString("ProxyDbConnectionString");
            var db = new MongoClient(url)
                .GetServer()
                .GetDatabase(url.DatabaseName);

            _collection = db.GetCollection<RequestResponsePair>("RequestResponsePair");
        }
        public void Handle(CreateApiLogItem command)
        {
            _collection.Insert(new RequestResponsePair
            {
                Id = CombGuid.Generate(),
                SessionId = command.SessionId,
                LogDate = DateTime.Now,
                ApiRequest = command.ApiRequest,
                ApiResponse = command.ApiResponse
            });
        }



        private MongoUrl GetConnectionString(string connectionStringName)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[connectionStringName];
            return MongoUrl.Create(connectionString != null ? connectionString.ConnectionString : connectionStringName);
        }

    }

    /// <summary>
    /// <see cref="https://github.com/Particular/NServiceBus/blob/develop/src/NServiceBus/IdGeneration/CombGuid.cs"/>
    /// </summary>
    public static class CombGuid
    {
        public static Guid Generate()
        {
            var guidArray = Guid.NewGuid().ToByteArray();

            var baseDate = new DateTime(1900, 1, 1);
            var now = DateTime.Now;

            // Get the days and milliseconds which will be used to build the byte string 
            var days = new TimeSpan(now.Ticks - baseDate.Ticks);
            var timeOfDay = now.TimeOfDay;

            // Convert to a byte array 
            // Note that SQL Server is accurate to 1/300th of a millisecond so we divide by 3.333333 
            var daysArray = BitConverter.GetBytes(days.Days);
            var millisecondArray = BitConverter.GetBytes((long)(timeOfDay.TotalMilliseconds / 3.333333));

            // Reverse the bytes to match SQL Servers ordering 
            Array.Reverse(daysArray);
            Array.Reverse(millisecondArray);

            // Copy the bytes into the guid 
            Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2);
            Array.Copy(millisecondArray, millisecondArray.Length - 4, guidArray, guidArray.Length - 4, 4);

            return new Guid(guidArray);
        }
    }
}
