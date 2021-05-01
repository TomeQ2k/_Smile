using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Logic.Responses.Command.Comment
{
    public class DeleteCommentResponse : BaseResponse
    {
        public DeleteCommentResponse(Error error = null) : base(error) { }
    }
}