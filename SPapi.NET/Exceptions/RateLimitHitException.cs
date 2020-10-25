using System;

namespace SPapi.NET.Exceptions
{
    [Serializable]
    internal class RateLimitHitException : Exception
    {
        internal RateLimitHitException() : base() {}
        internal RateLimitHitException(string message) : base(message) { }
        internal RateLimitHitException(string message, System.Exception inner) : base(message, inner) { }

        protected internal RateLimitHitException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}

