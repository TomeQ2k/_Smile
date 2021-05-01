using Smile.Core.Application.Dtos.Main;
using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Logic.Responses.Command.Post
{
    public class LikePostResponse : BaseResponse
    {
        public bool LikeCreated { get; set; }
        public LikeDto Like { get; set; }

        public LikePostResponse(Error error = null) : base(error) { }
    }
}