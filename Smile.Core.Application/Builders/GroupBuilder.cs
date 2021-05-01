using Smile.Core.Application.Builders.Interfaces;
using Smile.Core.Common.Enums.Permissions;
using Smile.Core.Domain.Entities.Group;

namespace Smile.Core.Application.Builders
{
    public class GroupBuilder : IGroupBuilder
    {
        private readonly Group group = new Group();

        public IGroupBuilder SetName(string name)
        {
            this.group.SetName(name);

            return this;
        }

        public IGroupBuilder WithDescription(string description)
        {
            this.group.SetDescription(description);

            return this;
        }

        public IGroupBuilder IsPrivate(bool isPrivate = false)
        {
            this.group.SetIsPrivate(isPrivate);

            return this;
        }

        public IGroupBuilder WithJoinCode(string joinCode = null)
        {
            this.group.SetJoinCode(joinCode);

            return this;
        }

        public IGroupBuilder WithPermissions(InviteMemberPermission inviteMemberPermission, RemoveMemberPermission removeMemberPermission)
        {
            this.group.SetPermissions(inviteMemberPermission, removeMemberPermission);

            return this;
        }

        public Group Build() => this.group;
    }
}