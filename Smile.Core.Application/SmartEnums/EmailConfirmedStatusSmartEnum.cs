using System.Linq;
using Ardalis.SmartEnum;
using Smile.Core.Common.Enums;
using Smile.Core.Domain.Entities.Auth;

namespace Smile.Core.Application.SmartEnums
{
    public abstract class EmailConfirmedStatusSmartEnum : SmartEnum<EmailConfirmedStatusSmartEnum>
    {
        protected EmailConfirmedStatusSmartEnum(string name, int value) : base(name, value) { }

        public static readonly EmailConfirmedStatusSmartEnum All = new AllType();
        public static readonly EmailConfirmedStatusSmartEnum Confirmed = new ConfirmedType();
        public static readonly EmailConfirmedStatusSmartEnum NotConfirmed = new NotConfirmedType();

        public abstract IQueryable<User> Filter(IQueryable<User> users);

        private sealed class AllType : EmailConfirmedStatusSmartEnum
        {
            public AllType() : base(nameof(All), (int)EmailConfirmedStatus.All) { }

            public override IQueryable<User> Filter(IQueryable<User> users) => users;
        }

        private sealed class ConfirmedType : EmailConfirmedStatusSmartEnum
        {
            public ConfirmedType() : base(nameof(Confirmed), (int)EmailConfirmedStatus.Confirmed) { }

            public override IQueryable<User> Filter(IQueryable<User> users)
                => users.Where(u => u.EmailConfirmed);
        }

        private sealed class NotConfirmedType : EmailConfirmedStatusSmartEnum
        {
            public NotConfirmedType() : base(nameof(NotConfirmed), (int)EmailConfirmedStatus.NotConfirmed) { }

            public override IQueryable<User> Filter(IQueryable<User> users)
                => users.Where(u => !u.EmailConfirmed);
        }
    }
}