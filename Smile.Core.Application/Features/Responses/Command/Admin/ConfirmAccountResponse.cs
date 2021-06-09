using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Features.Responses.Command.Admin
{
    public class ConfirmAccountResponse : BaseResponse
    {
        public ConfirmAccountResponse(Error error = null) : base(error) { }
    }
}