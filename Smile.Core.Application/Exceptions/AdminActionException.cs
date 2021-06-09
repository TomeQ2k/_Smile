using System;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Application.Exceptions
{
    public class AdminActionException : ApplicationException
    {
        public string ErrorCode { get; }

        public AdminActionException(string message, string errorCode = ErrorCodes.AdminActionFailed) : base(message)
            => (ErrorCode) = (errorCode);
    }
}