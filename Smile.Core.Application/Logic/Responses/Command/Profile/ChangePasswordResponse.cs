using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Logic.Responses.Command.Profile
{
    public class ChangePasswordResponse : BaseResponse
    {
        public ChangePasswordResponse(Error error = null) : base(error) { }
    }
}