using System.Collections.Generic;
using Smile.Core.Application.Dtos.Post;
using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Logic.Responses.Query.Post
{
    public class GetPostsPaginationResponse : BaseResponse
    {
        public List<PostDto> Posts { get; set; }

        public GetPostsPaginationResponse(Error error = null) : base(error) { }
    }
}