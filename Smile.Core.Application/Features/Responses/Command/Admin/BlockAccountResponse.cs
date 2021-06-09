using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Features.Responses.Command.Admin
{
    public class BlockAccountResponse : BaseResponse
    {
        public bool IsBlocked { get; set; }

        public BlockAccountResponse(Error error = null) : base(error) { }
    }
}