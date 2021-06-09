using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Features.Responses.Command.Story
{
    public class WatchStoryResponse : BaseResponse
    {
        public WatchStoryResponse(Error error = null) : base(error) { }
    }
}