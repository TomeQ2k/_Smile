using System;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Application.Exceptions
{
    public class ChangePasswordException : Exception
    {
        public string ErrorCode { get; }

        public ChangePasswordException(string message, string errorCode = ErrorCodes.ChangePasswordFailed) : base(message)
            => (ErrorCode) = (errorCode);
    }
}