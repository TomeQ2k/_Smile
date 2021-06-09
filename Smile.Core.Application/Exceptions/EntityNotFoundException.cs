using System;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Application.Exceptions
{
    public class EntityNotFoundException : ApplicationException
    {
        public string ErrorCode { get; }

        public EntityNotFoundException(string message, string errorCode = ErrorCodes.EntityNotFound) : base(message)
            => (ErrorCode) = (errorCode);
    }
}