using System.Threading.Tasks;
using Smile.Core.Application.Features.Requests.Command.GroupManager;
using Smile.Core.Application.Results;
using Smile.Core.Application.Services.ReadOnly;
using Smile.Core.Domain.Entities.Group;

namespace Smile.Core.Application.Services
{
    public interface IGroupManager : IReadOnlyGroupManager
    {
        Task<InviteMemberResult> InviteMember(string groupId, string userId);
        Task<GroupMember> AcceptMember(string groupId, string userId, bool accept = true);

        Task<bool> KickMember(string groupId, string userId);
        Task<bool> LeaveGroup(string groupId);

        Task<Group> UpdateGroup(string groupId, UpdateGroupRequest request);
        Task<bool> DeleteGroup(string groupId);

        Task<bool> SetModerator(string groupId, string userId, bool isModerator = true);
    }
}