using System.Linq;
using Ardalis.SmartEnum;
using Smile.Core.Common.Enums;
using Smile.Core.Domain.Entities.Group;

namespace Smile.Core.Application.SmartEnums
{
    public abstract class GroupSortTypeSmartEnum : SmartEnum<GroupSortTypeSmartEnum>
    {
        protected GroupSortTypeSmartEnum(string name, int value) : base(name, value) { }

        public static readonly GroupSortTypeSmartEnum CreatedDescending = new CreatedDescendingType();
        public static readonly GroupSortTypeSmartEnum CreatedAscending = new CreatedAscendingType();
        public static readonly GroupSortTypeSmartEnum MembersCountDescending = new MembersCountDescendingType();
        public static readonly GroupSortTypeSmartEnum MembersCountAscending = new MembersCountAscendingType();

        public abstract IQueryable<Group> Sort(IQueryable<Group> groups);

        private sealed class CreatedDescendingType : GroupSortTypeSmartEnum
        {
            public CreatedDescendingType() : base(nameof(CreatedDescending), (int)GroupSortType.CreatedDescending) { }

            public override IQueryable<Group> Sort(IQueryable<Group> groups)
                => groups.OrderByDescending(g => g.DateCreated);
        }

        private sealed class CreatedAscendingType : GroupSortTypeSmartEnum
        {
            public CreatedAscendingType() : base(nameof(CreatedAscending), (int)GroupSortType.CreatedAscending) { }

            public override IQueryable<Group> Sort(IQueryable<Group> groups)
                => groups.OrderBy(g => g.DateCreated);
        }

        private sealed class MembersCountDescendingType : GroupSortTypeSmartEnum
        {
            public MembersCountDescendingType() : base(nameof(MembersCountDescending), (int)GroupSortType.MembersCountDescending) { }

            public override IQueryable<Group> Sort(IQueryable<Group> groups)
                => groups.OrderByDescending(g => g.GetMembersCount());
        }

        private sealed class MembersCountAscendingType : GroupSortTypeSmartEnum
        {
            public MembersCountAscendingType() : base(nameof(MembersCountAscending), (int)GroupSortType.MembersCountAscending) { }

            public override IQueryable<Group> Sort(IQueryable<Group> groups)
                => groups.OrderBy(g => g.GetMembersCount());
        }
    }
}