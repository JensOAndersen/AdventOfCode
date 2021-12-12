using System;
using System.Runtime.Serialization;

namespace AoC.Core.Dep
{
    internal class TypeAlreadyRegisteredException : Exception
    {
        public TypeAlreadyRegisteredException()
        {
        }

        public TypeAlreadyRegisteredException(string message) : base(message)
        {
        }

        public TypeAlreadyRegisteredException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TypeAlreadyRegisteredException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
