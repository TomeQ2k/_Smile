using System.Collections.Generic;
using System.Linq;
using Ardalis.SmartEnum;
using Smile.Core.Common.Enums;
using Smile.Core.Domain.Entities.Group;

namespace Smile.Core.Application.SmartEnums
{
    public abstract class GroupAccessStatusSmartEnum : SmartEnum<GroupAccessStatusSmartEnum>
    {
        protected GroupAccessStatusSmartEnum(string name, int value) : base(name, value) { }

        public static readonly GroupAccessStatusSmartEnum All = new AllType();
        public static readonly GroupAccessStatusSmartEnum OnlyPrivate = new OnlyPrivateType();
        public static readonly GroupAccessStatusSmartEnum OnlyPublic = new OnlyPublicType();

        public abstract IEnumerable<Group> Filter(IEnumerable<Group> groups);

        private sealed class AllType : GroupAccessStatusSmartEnum
        {
            public AllType() : base(nameof(All), (int)GroupAccessStatus.All) { }

            public override IEnumerable<Group> Filter(IEnumerable<Group> groups) => groups;
        }

        private sealed class OnlyPrivateType : GroupAccessStatusSmartEnum
        {
            public OnlyPrivateType() : base(nameof(OnlyPrivate), (int)GroupAccessStatus.OnlyPrivate) { }

            public override IEnumerable<Group> Filter(IEnumerable<Group> groups)
                => groups.Where(g => g.IsPrivate);
        }

        private sealed class OnlyPublicType : GroupAccessStatusSmartEnum
        {
            public OnlyPublicType() : base(nameof(OnlyPublic), (int)GroupAccessStatus.OnlyPublic) { }

            public override IEnumerable<Group> Filter(IEnumerable<Group> groups)
                => groups.Where(g => !g.IsPrivate);
        }
    }
}