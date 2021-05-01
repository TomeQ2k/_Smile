using Smile.Core.Application.Dtos.Main;
using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Logic.Responses.Query.Post
{
    public class GetPostResponse : BaseResponse
    {
        public PostDto Post { get; set; }

        public GetPostResponse(Error error = null) : base(error) { }
    }
}