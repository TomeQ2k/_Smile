using System;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Application.Exceptions
{
    public class DuplicateException : Exception
    {
        public string ErrorCode { get; }

        public DuplicateException(string message, string errorCode = ErrorCodes.DuplicateExists) : base(message)
            => (ErrorCode) = (errorCode);
    }
}