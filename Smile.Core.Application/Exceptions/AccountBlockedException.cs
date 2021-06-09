using System;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Application.Exceptions
{
    public class AccountBlockedException : ApplicationException
    {
        public string ErrorCode { get; }

        public AccountBlockedException(string message, string errorCode = ErrorCodes.AccountBlocked) : base(message)
            => (ErrorCode) = (errorCode);
    }
}