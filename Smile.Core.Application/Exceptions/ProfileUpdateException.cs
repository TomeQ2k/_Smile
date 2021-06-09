using System;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Application.Exceptions
{
    public class ProfileUpdateException : ApplicationException
    {
        public string ErrorCode { get; }

        public ProfileUpdateException(string message, string errorCode = ErrorCodes.ProfileUpdateError) : base(message)
            => (ErrorCode) = (errorCode);
    }
}