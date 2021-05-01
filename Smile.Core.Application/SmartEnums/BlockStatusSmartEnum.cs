using System.Linq;
using Ardalis.SmartEnum;
using Smile.Core.Common.Enums;
using Smile.Core.Domain.Entities.Auth;

namespace Smile.Core.Application.SmartEnums
{
    public abstract class BlockStatusSmartEnum : SmartEnum<BlockStatusSmartEnum>
    {
        protected BlockStatusSmartEnum(string name, int value) : base(name, value) { }

        public static readonly BlockStatusSmartEnum All = new AllType();
        public static readonly BlockStatusSmartEnum NotBlocked = new NotBlockedType();
        public static readonly BlockStatusSmartEnum Blocked = new BlockedType();

        public abstract IQueryable<User> Filter(IQueryable<User> users);

        private sealed class AllType : BlockStatusSmartEnum
        {
            public AllType() : base(nameof(All), (int)BlockStatus.All) { }

            public override IQueryable<User> Filter(IQueryable<User> users) => users;
        }

        private sealed class NotBlockedType : BlockStatusSmartEnum
        {
            public NotBlockedType() : base(nameof(NotBlocked), (int)BlockStatus.NotBlocked) { }

            public override IQueryable<User> Filter(IQueryable<User> users)
                => users.Where(u => !u.IsBlocked);
        }

        private sealed class BlockedType : BlockStatusSmartEnum
        {
            public BlockedType() : base(nameof(Blocked), (int)BlockStatus.Blocked) { }

            public override IQueryable<User> Filter(IQueryable<User> users)
                => users.Where(u => u.IsBlocked);
        }
    }
}