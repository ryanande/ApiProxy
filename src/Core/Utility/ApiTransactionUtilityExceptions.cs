using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EdFiValidation.ApiProxy.Core.Utility
{
    [Serializable]
    public class CannotParseUriException : Exception
    {
        public static readonly string ExpectedFormatMsg = 
            "Expected URI format is /api/{sessionId}/{Base64EncodedDestination}/{clientId}/{DistionationAction}/{ActionParameters}...";
        //todo : update the spec to get it in sync with the above format... after confirming the above is correct :P

        public CannotParseUriException()
            : base("Error parsing URI.")
        {
        }

        public CannotParseUriException(string message)
            : base(message)
        {
        }

        public CannotParseUriException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }

        public CannotParseUriException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected CannotParseUriException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
