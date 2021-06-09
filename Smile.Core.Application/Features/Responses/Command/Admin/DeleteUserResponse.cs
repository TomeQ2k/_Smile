using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Features.Responses.Command.Admin
{
    public class DeleteUserResponse : BaseResponse
    {
        public DeleteUserResponse(Error error = null) : base(error) { }
    }
}