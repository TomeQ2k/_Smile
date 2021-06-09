using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Features.Responses.Command.Profile
{
    public class SetAvatarResponse : BaseResponse
    {
        public string AvatarUrl { get; set; }

        public SetAvatarResponse(Error error = null) : base(error) { }
    }
}