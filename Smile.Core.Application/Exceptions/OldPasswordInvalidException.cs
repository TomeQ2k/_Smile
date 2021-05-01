using System;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Application.Exceptions
{
    public class OldPasswordInvalidException : Exception
    {
        public string ErrorCode { get; }

        public OldPasswordInvalidException(string message, string errorCode = ErrorCodes.OldPasswordInvalid) : base(message)
            => (ErrorCode) = (errorCode);
    }
}