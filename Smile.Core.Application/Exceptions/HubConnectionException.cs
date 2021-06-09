using System;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Application.Exceptions
{
    public class HubConnectionException : ApplicationException
    {
        public string ErrorCode { get; }

        public HubConnectionException(string message, string errorCode = ErrorCodes.HubConnectionFailed) : base(message)
            => (ErrorCode) = (errorCode);
    }
}