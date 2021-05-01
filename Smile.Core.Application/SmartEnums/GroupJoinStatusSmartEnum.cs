using System.Collections.Generic;
using System.Linq;
using Ardalis.SmartEnum;
using Smile.Core.Common.Enums;
using Smile.Core.Domain.Entities.Group;

namespace Smile.Core.Application.SmartEnums
{
    public abstract class GroupJoinStatusSmartEnum : SmartEnum<GroupJoinStatusSmartEnum>
    {
        protected GroupJoinStatusSmartEnum(string name, int value) : base(name, value) { }

        public static readonly GroupJoinStatusSmartEnum All = new AllType();
        public static readonly GroupJoinStatusSmartEnum MemberGroups = new MemberGroupsType();
        public static readonly GroupJoinStatusSmartEnum OwnerGroups = new OwnerGroupsType();

        public abstract IEnumerable<Group> Filter(IEnumerable<Group> groups, string userId);

        private sealed class AllType : GroupJoinStatusSmartEnum
        {
            public AllType() : base(nameof(All), (int)GroupJoinStatus.All) { }

            public override IEnumerable<Group> Filter(IEnumerable<Group> groups, string userId) => groups;
        }

        private sealed class MemberGroupsType : GroupJoinStatusSmartEnum
        {
            public MemberGroupsType() : base(nameof(MemberGroups), (int)GroupJoinStatus.MemberGroups) { }

            public override IEnumerable<Group> Filter(IEnumerable<Group> groups, string userId)
                => groups.Where(g => g.GroupMembers.Any(m => m.UserId == userId && m.IsAccepted));
        }

        private sealed class OwnerGroupsType : GroupJoinStatusSmartEnum
        {
            public OwnerGroupsType() : base(nameof(OwnerGroups), (int)GroupJoinStatus.OwnerGroups) { }

            public override IEnumerable<Group> Filter(IEnumerable<Group> groups, string userId)
                => groups.Where(g => g.AdminId == userId);
        }
    }
}