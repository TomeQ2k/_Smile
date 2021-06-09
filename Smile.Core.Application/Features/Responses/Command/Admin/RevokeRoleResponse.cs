using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Features.Responses.Command.Admin
{
    public class RevokeRoleResponse : BaseResponse
    {
        public RevokeRoleResponse(Error error = null) : base(error) { }
    }
}