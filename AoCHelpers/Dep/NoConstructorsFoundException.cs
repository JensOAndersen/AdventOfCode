using System;
using System.Runtime.Serialization;

namespace AoC.Core.Dep
{
    internal class NoConstructorsFoundException : Exception
    {
        public NoConstructorsFoundException()
        {
        }

        public NoConstructorsFoundException(string message) : base(message)
        {
        }

        public NoConstructorsFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoConstructorsFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
