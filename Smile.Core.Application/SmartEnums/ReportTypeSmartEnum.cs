using System.Linq;
using Ardalis.SmartEnum;
using Smile.Core.Common.Enums;
using Smile.Core.Domain.Entities.Support;

namespace Smile.Core.Application.SmartEnums
{
    public abstract class ReportTypeSmartEnum : SmartEnum<ReportTypeSmartEnum>
    {
        protected ReportTypeSmartEnum(string name, int value) : base(name, value) { }

        public static readonly ReportTypeSmartEnum All = new AllType();
        public static readonly ReportTypeSmartEnum Authorized = new AuthorizedType();
        public static readonly ReportTypeSmartEnum Anonymous = new AnonymousType();

        public abstract IQueryable<Report> Filter(IQueryable<Report> reports);

        private sealed class AllType : ReportTypeSmartEnum
        {
            public AllType() : base(nameof(All), (int)ReportType.All) { }

            public override IQueryable<Report> Filter(IQueryable<Report> reports) => reports;
        }

        private sealed class AuthorizedType : ReportTypeSmartEnum
        {
            public AuthorizedType() : base(nameof(Authorized), (int)ReportType.Authorized) { }

            public override IQueryable<Report> Filter(IQueryable<Report> reports)
                => reports.Where(r => r.ReporterId != null);
        }

        private sealed class AnonymousType : ReportTypeSmartEnum
        {
            public AnonymousType() : base(nameof(All), (int)ReportType.Anonymous) { }

            public override IQueryable<Report> Filter(IQueryable<Report> reports)
                => reports.Where(r => r.ReporterId == null);
        }
    }
}