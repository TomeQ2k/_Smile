using System;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Application.Exceptions
{
    public class TokenException : ApplicationException
    {
        public string ErrorCode { get; }

        public TokenException(string message, string errorCode = ErrorCodes.TokenInvalid) : base(message)
            => (ErrorCode) = (errorCode);
    }
}