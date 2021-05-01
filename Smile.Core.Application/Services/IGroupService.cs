using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Smile.Core.Application.Services.ReadOnly;
using Smile.Core.Common.Enums.Permissions;
using Smile.Core.Domain.Entities.Group;

namespace Smile.Core.Application.Services
{
    public interface IGroupService : IReadOnlyGroupService
    {
        Task<Group> CreateGroup(string name, string description, bool isPrivate, IFormFile image, string joinCode, InviteMemberPermission inviteMemberPermission, RemoveMemberPermission removeMemberPermission);

        Task<GroupMember> JoinGroup(string groupId, string joinCode = null);
    }
}