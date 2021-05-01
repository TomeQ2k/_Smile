using Smile.Core.Application.Dtos.Story;
using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Logic.Responses.Command.Story
{
    public class AddStoryResponse : BaseResponse
    {
        public StoryDto Story { get; set; }

        public AddStoryResponse(Error error = null) : base(error) { }
    }
}