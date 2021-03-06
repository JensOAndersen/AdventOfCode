using System;
using System.Runtime.Serialization;

namespace AoC.Core.Dep
{
    internal class TypeNotRegisteredException : Exception
    {
        public TypeNotRegisteredException()
        {
        }

        public TypeNotRegisteredException(string message) : base(message)
        {
        }

        public TypeNotRegisteredException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TypeNotRegisteredException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
