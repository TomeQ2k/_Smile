using System;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Application.Exceptions
{
    public class BlockException : ApplicationException
    {
        public string ErrorCode { get; }

        public BlockException(string message, string errorCode = ErrorCodes.AccountBlocked) : base(message)
            => (ErrorCode) = (errorCode);
    }
}