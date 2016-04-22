using System;

namespace Simple.Common.Exceptions
{
    public class OptimsticConcurrencyException : Exception
    {
        public OptimsticConcurrencyException(string message) : base(message) {}
    }
}