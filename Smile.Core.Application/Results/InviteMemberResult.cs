using Smile.Core.Domain.Entities.Group;

namespace Smile.Core.Application.Results
{
    public class InviteMemberResult
    {
        public GroupMember Member { get; }
        public GroupInvite Invite { get; }

        public InviteMemberResult(GroupMember member, GroupInvite invite)
        {
            Member = member;
            Invite = invite;
        }
    }
}