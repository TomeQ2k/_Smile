using System.Collections.Generic;
using System.Linq;
using Ardalis.SmartEnum;
using Smile.Core.Common.Enums;
using Smile.Core.Domain.Entities.Auth;

namespace Smile.Core.Application.SmartEnums
{
    public abstract class UserSortTypeSmartEnum : SmartEnum<UserSortTypeSmartEnum>
    {
        protected UserSortTypeSmartEnum(string name, int value) : base(name, value) { }

        public static readonly UserSortTypeSmartEnum Descending = new DescendingType();
        public static readonly UserSortTypeSmartEnum Ascending = new AscendingType();

        public abstract IEnumerable<User> Sort(IEnumerable<User> users);

        private sealed class DescendingType : UserSortTypeSmartEnum
        {
            public DescendingType() : base(nameof(Descending), (int)SortType.Descending) { }

            public override IEnumerable<User> Sort(IEnumerable<User> users)
                => users.OrderByDescending(u => u.DateRegistered);
        }

        private sealed class AscendingType : UserSortTypeSmartEnum
        {
            public AscendingType() : base(nameof(Ascending), (int)SortType.Ascending) { }

            public override IEnumerable<User> Sort(IEnumerable<User> users)
                => users.OrderBy(u => u.DateRegistered);
        }
    }
}