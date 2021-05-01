using System;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Application.Exceptions
{
    public class UploadFileException : Exception
    {
        public string ErrorCode { get; }

        public UploadFileException(string message, string errorCode = ErrorCodes.UploadFileFailed) : base(message)
            => (ErrorCode) = (errorCode);
    }
}