using Smile.Core.Application.Dtos.Post;
using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Features.Responses.Command.Post
{
    public class LikePostResponse : BaseResponse
    {
        public bool LikeCreated { get; set; }
        public LikeDto Like { get; set; }

        public LikePostResponse(Error error = null) : base(error) { }
    }
}