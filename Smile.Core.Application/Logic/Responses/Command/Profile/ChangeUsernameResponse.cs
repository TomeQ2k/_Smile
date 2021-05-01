using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Logic.Responses.Command.Profile
{
    public class ChangeUsernameResponse : BaseResponse
    {
        public ChangeUsernameResponse(Error error = null) : base(error) { }
    }
}