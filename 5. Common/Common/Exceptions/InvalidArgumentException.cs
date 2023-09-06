using System;

namespace Common.Exceptions
{
    public class InvalidArgumentException : Exception
    {
        public string ErrorCode;

        public InvalidArgumentException() : base("Invalid paramter(s)")
        {
        }

        public InvalidArgumentException(string errorMessage) : base(errorMessage)
        {
        }
        public InvalidArgumentException(string errorMessage, string errorCode) : base(errorMessage)
        {
            ErrorCode = errorCode;
        }
    }
}
