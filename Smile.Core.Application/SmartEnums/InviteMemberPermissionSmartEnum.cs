using System.Linq;
using Ardalis.SmartEnum;
using Smile.Core.Common.Enums.Permissions;
using Smile.Core.Domain.Entities.Group;

namespace Smile.Core.Application.SmartEnums
{
    public abstract class InviteMemberPermissionSmartEnum : SmartEnum<InviteMemberPermissionSmartEnum>
    {
        protected InviteMemberPermissionSmartEnum(string name, int value) : base(name, value) { }

        public static readonly InviteMemberPermissionSmartEnum Admin = new AdminType();
        public static readonly InviteMemberPermissionSmartEnum Moderator = new ModeratorType();
        public static readonly InviteMemberPermissionSmartEnum All = new AllType();

        public abstract bool ValidatePermission(string currentUserId, Group group);

        private sealed class AdminType : InviteMemberPermissionSmartEnum
        {
            public AdminType() : base(nameof(Admin), (int)InviteMemberPermission.Admin) { }

            public override bool ValidatePermission(string currentUserId, Group group)
                => currentUserId == group.AdminId;
        }

        private sealed class ModeratorType : InviteMemberPermissionSmartEnum
        {
            public ModeratorType() : base(nameof(Moderator), (int)InviteMemberPermission.Moderator) { }

            public override bool ValidatePermission(string currentUserId, Group group)
                => currentUserId == group.AdminId || group.GroupMembers.Any(m => m.IsModerator && m.UserId == currentUserId);
        }

        private sealed class AllType : InviteMemberPermissionSmartEnum
        {
            public AllType() : base(nameof(All), (int)InviteMemberPermission.All) { }

            public override bool ValidatePermission(string currentUserId, Group group)
                => currentUserId == group.AdminId || group.GroupMembers.Any(m => m.UserId == currentUserId);
        }
    }
}