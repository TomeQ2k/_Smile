using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Features.Responses.Command.Post
{
    public class DeletePostResponse : BaseResponse
    {
        public DeletePostResponse(Error error = null) : base(error) { }
    }
}