using System;

namespace Note.Api.Exceptions
{
    public class ModelStateException : Exception
    {
        public ModelStateException(string message) : base(message) { }
    }
}
