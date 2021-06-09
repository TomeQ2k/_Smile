using System;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Application.Exceptions
{
    public class CrudException : ApplicationException
    {
        public string ErrorCode { get; }

        public CrudException(string message, string errorCode = ErrorCodes.CrudActionFailed) : base(message)
            => (ErrorCode) = (errorCode);
    }
}