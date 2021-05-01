using Smile.Core.Application.Dtos.Group;
using Smile.Core.Application.Models.Error;

namespace Smile.Core.Application.Logic.Responses.Command.GroupManager
{
    public class InviteMemberResponse : BaseResponse
    {
        public GroupMemberDto Member { get; set; }
        public GroupInviteDto Invite { get; set; }

        public InviteMemberResponse(Error error = null) : base(error) { }
    }
}