using System;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Application.Exceptions
{
    public class ServerException : Exception
    {
        public string ErrorCode { get; }

        public ServerException(string message, string errorCode = ErrorCodes.ServerError) : base(message)
            => (ErrorCode) = (errorCode);
    }
}