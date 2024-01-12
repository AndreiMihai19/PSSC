using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace proiectpssc.Exceptions
{
    [Serializable]
    internal class InvalidProductIDException : Exception
    {
        public InvalidProductIDException()
        {
        }

        public InvalidProductIDException(string? message) : base(message)
        {
        }

        public InvalidProductIDException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidProductIDException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
