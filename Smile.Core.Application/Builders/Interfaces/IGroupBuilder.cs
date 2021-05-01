using Smile.Core.Common.Enums.Permissions;
using Smile.Core.Domain.Entities.Group;

namespace Smile.Core.Application.Builders.Interfaces
{
    public interface IGroupBuilder : IBuilder<Group>
    {
        IGroupBuilder SetName(string name);
        IGroupBuilder WithDescription(string description);
        IGroupBuilder IsPrivate(bool isPrivate = false);
        IGroupBuilder WithJoinCode(string joinCode = null);
        IGroupBuilder WithPermissions(InviteMemberPermission inviteMemberPermission, RemoveMemberPermission removeMemberPermission);
    }
}