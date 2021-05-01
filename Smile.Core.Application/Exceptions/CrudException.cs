using System;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Application.Exceptions
{
    public class CrudException : Exception
    {
        public string ErrorCode { get; }

        public CrudException(string message, string errorCode = ErrorCodes.CrudActionFailed) : base(message)
            => (ErrorCode) = (errorCode);
    }
}