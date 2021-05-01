using Smile.Core.Application.Dtos.Main;
using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Logic.Responses.Command.Comment
{
    public class CreateCommentResponse : BaseResponse
    {
        public CommentDto Comment { get; set; }

        public CreateCommentResponse(Error error = null) : base(error) { }
    }
}