using System.Linq;
using Ardalis.SmartEnum;
using Smile.Core.Common.Enums.Permissions;
using Smile.Core.Domain.Entities.Group;

namespace Smile.Core.Application.SmartEnums
{
    public abstract class RemoveMemberPermissionSmartEnum : SmartEnum<RemoveMemberPermissionSmartEnum>
    {
        protected RemoveMemberPermissionSmartEnum(string name, int value) : base(name, value) { }

        public static readonly RemoveMemberPermissionSmartEnum Admin = new AdminType();
        public static readonly RemoveMemberPermissionSmartEnum Moderator = new ModeratorType();

        public abstract bool ValidatePermission(string currentUserId, Group group);

        private sealed class AdminType : RemoveMemberPermissionSmartEnum
        {
            public AdminType() : base(nameof(Admin), (int)RemoveMemberPermission.Admin) { }

            public override bool ValidatePermission(string currentUserId, Group group)
                => currentUserId == group.AdminId;
        }

        private sealed class ModeratorType : RemoveMemberPermissionSmartEnum
        {
            public ModeratorType() : base(nameof(Moderator), (int)RemoveMemberPermission.Moderator) { }

            public override bool ValidatePermission(string currentUserId, Group group)
                => currentUserId == group.AdminId || group.GroupMembers.Any(m => m.IsModerator && m.UserId == currentUserId);
        }
    }
}