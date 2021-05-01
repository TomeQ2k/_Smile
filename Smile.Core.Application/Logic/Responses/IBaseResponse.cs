using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Logic.Responses
{
    public interface IBaseResponse
    {
        bool IsSucceeded { get; }

        Error Error { get; }
    }
}