using System;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Application.Exceptions
{
    public class NoPermissionsException : Exception
    {
        public string ErrorCode { get; }

        public NoPermissionsException(string message, string errorCode = ErrorCodes.AccessDenied) : base(message)
            => (ErrorCode) = (errorCode);
    }
}