using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace proiectpssc.Exceptions
{
    [Serializable]
    internal class InvalidClientIDException : Exception
    {
        public InvalidClientIDException()
        {
        }

        public InvalidClientIDException(string? message) : base(message)
        {
        }

        public InvalidClientIDException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidClientIDException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
