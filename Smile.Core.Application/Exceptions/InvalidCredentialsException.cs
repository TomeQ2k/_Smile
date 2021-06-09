using System;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Application.Exceptions
{
    public class InvalidCredentialsException : ApplicationException
    {
        public string ErrorCode { get; }

        public InvalidCredentialsException(string message, string errorCode = ErrorCodes.InvalidCredentials) : base(message)
            => (ErrorCode) = (errorCode);
    }
}