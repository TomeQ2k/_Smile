using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Logic.Responses.Command.Post
{
    public class DeletePostResponse : BaseResponse
    {
        public DeletePostResponse(Error error = null) : base(error) { }
    }
}