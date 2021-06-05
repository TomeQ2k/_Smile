using Smile.Core.Application.Dtos.Post;
using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Logic.Responses.Command.Post
{
    public class CreatePostResponse : BaseResponse
    {
        public PostDto Post { get; set; }

        public CreatePostResponse(Error error = null) : base(error) { }
    }
}