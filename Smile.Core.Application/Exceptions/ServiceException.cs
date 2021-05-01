using System;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Application.Exceptions
{
    public class ServiceException : Exception
    {
        public string ErrorCode { get; }

        public ServiceException(string message, string errorCode = ErrorCodes.ServiceError) : base(message)
            => (ErrorCode) = (errorCode);
    }
}