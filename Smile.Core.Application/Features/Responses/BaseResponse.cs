using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Features.Responses
{
    public class BaseResponse : IBaseResponse
    {
        public bool IsSucceeded { get; }

        public Error Error { get; }

        public BaseResponse(Error error = null)
        {
            Error = error;

            IsSucceeded = Error == null;
        }
    }
}