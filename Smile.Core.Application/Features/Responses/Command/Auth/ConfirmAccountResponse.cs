using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Features.Responses.Command.Auth
{
    public class ConfirmAccountResponse : BaseResponse
    {
        public ConfirmAccountResponse(Error error = null) : base(error) { }
    }
}