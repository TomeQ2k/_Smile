using Smile.Core.Application.Dtos.Post;
using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Features.Responses.Query.Post
{
    public class GetPostResponse : BaseResponse
    {
        public PostDto Post { get; set; }

        public GetPostResponse(Error error = null) : base(error) { }
    }
}