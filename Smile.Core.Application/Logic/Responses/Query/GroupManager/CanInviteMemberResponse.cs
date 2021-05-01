using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Logic.Responses.Query.GroupManager
{
    public class CanInviteMemberResponse : BaseResponse
    {
        public bool CanInvite { get; set; }
        public string UserId { get; set; }

        public CanInviteMemberResponse(Error error = null) : base(error) { }
    }
}