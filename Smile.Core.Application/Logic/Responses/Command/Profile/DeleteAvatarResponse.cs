using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Logic.Responses.Command.Profile
{
    public class DeleteAvatarResponse : BaseResponse
    {
        public DeleteAvatarResponse(Error error = null) : base(error) { }
    }
}