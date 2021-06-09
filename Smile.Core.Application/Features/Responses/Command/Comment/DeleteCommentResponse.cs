using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Features.Responses.Command.Comment
{
    public class DeleteCommentResponse : BaseResponse
    {
        public DeleteCommentResponse(Error error = null) : base(error) { }
    }
}