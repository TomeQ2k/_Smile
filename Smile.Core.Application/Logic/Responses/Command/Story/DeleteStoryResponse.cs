using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Logic.Responses.Command.Story
{
    public class DeleteStoryResponse : BaseResponse
    {
        public DeleteStoryResponse(Error error = null) : base(error) { }
    }
}