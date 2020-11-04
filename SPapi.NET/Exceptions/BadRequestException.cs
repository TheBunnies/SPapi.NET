using System;

namespace SPapi.NET.Exceptions
{
    [Serializable]
    public class BadRequestException : Exception
    {
        internal BadRequestException() : base() {}
        internal BadRequestException(string message) : base(message) { }
        internal BadRequestException(string message, System.Exception inner) : base(message, inner) { }

        protected internal BadRequestException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
