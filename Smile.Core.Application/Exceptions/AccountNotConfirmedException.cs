using System;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Application.Exceptions
{
    public class AccountNotConfirmedException : Exception
    {
        public string ErrorCode { get; }

        public AccountNotConfirmedException(string message, string errorCode = ErrorCodes.AccountNotConfirmed) : base(message)
            => (ErrorCode) = (errorCode);
    }
}