using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Logic.Responses.Command.Auth
{
    public class ConfirmAccountResponse : BaseResponse
    {
        public ConfirmAccountResponse(Error error = null) : base(error) { }
    }
}