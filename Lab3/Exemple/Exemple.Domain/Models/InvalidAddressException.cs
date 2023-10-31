using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Exemple.Domain.Models
{
    internal class InvalidAddressException : Exception
    {
            public InvalidAddressException()
            {
            }

            public InvalidAddressException(string? message) : base(message)
            {
            }

            public InvalidAddressException(string? message, Exception? innerException) : base(message, innerException)
            {
            }

            protected InvalidAddressException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
    }
}
