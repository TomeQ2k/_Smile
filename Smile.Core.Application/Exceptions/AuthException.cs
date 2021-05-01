using System;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Application.Exceptions
{
    public class AuthException : Exception
    {
        public string ErrorCode { get; }

        public AuthException(string message, string errorCode = ErrorCodes.AuthError) : base(message)
            => (ErrorCode) = (errorCode);
    }
}