using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Features.Responses.Command.Profile
{
    public class ChangeUsernameResponse : BaseResponse
    {
        public ChangeUsernameResponse(Error error = null) : base(error) { }
    }
}