using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Features.Responses.Command.Profile
{
    public class ChangeEmailResponse : BaseResponse
    {
        public ChangeEmailResponse(Error error = null) : base(error) { }
    }
}