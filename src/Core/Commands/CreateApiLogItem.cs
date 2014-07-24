using System;
using EdFiValidation.ApiProxy.Core.Models;

namespace EdFiValidation.ApiProxy.Core.Commands
{
    public class CreateApiLogItem : ICommand
    {
        public Guid Id { get; set; }
        public string SessionId { get; set; }
        public ApiRequest ApiRequest { get; set; }
        public ApiResponse ApiResponse { get; set; }
    }
}
