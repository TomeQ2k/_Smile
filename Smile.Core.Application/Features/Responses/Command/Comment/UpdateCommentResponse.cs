using Smile.Core.Application.Dtos.Comment;
using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Features.Responses.Command.Comment
{
    public class UpdateCommentResponse : BaseResponse
    {
        public CommentDto Comment { get; set; }

        public UpdateCommentResponse(Error error = null) : base(error) { }
    }
}