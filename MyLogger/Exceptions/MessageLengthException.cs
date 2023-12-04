using System;

namespace MyLogger.Exceptions
{
    public class MessageLengthException : Exception
    {
        public MessageLengthException(string message)
            : base(message)
        {
        }
    }
}
