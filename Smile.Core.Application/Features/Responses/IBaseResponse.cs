using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Features.Responses
{
    public interface IBaseResponse
    {
        bool IsSucceeded { get; }

        Error Error { get; }
    }
}