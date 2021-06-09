using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Features.Responses.Command.Profile
{
    public class GenerateChangeEmailTokenResponse : BaseResponse
    {
        public GenerateChangeEmailTokenResponse(Error error = null) : base(error) { }
    }
}