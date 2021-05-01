using System.Linq;
using Ardalis.SmartEnum;
using Smile.Core.Common.Enums;
using Smile.Core.Domain.Entities.Support;

namespace Smile.Core.Application.SmartEnums
{
    public abstract class ReportStatusSmartEnum : SmartEnum<ReportStatusSmartEnum>
    {
        protected ReportStatusSmartEnum(string name, int value) : base(name, value) { }

        public static readonly ReportStatusSmartEnum All = new AllType();
        public static readonly ReportStatusSmartEnum Closed = new ClosedType();
        public static readonly ReportStatusSmartEnum NotClosed = new NotClosedType();

        public abstract IQueryable<Report> Filter(IQueryable<Report> reports);

        private sealed class AllType : ReportStatusSmartEnum
        {
            public AllType() : base(nameof(All), (int)ReportStatus.All) { }

            public override IQueryable<Report> Filter(IQueryable<Report> reports) => reports;
        }

        private sealed class ClosedType : ReportStatusSmartEnum
        {
            public ClosedType() : base(nameof(Closed), (int)ReportStatus.Closed) { }

            public override IQueryable<Report> Filter(IQueryable<Report> reports)
                => reports.Where(r => r.IsClosed);
        }

        private sealed class NotClosedType : ReportStatusSmartEnum
        {
            public NotClosedType() : base(nameof(NotClosed), (int)ReportStatus.NotClosed) { }

            public override IQueryable<Report> Filter(IQueryable<Report> reports)
                => reports.Where(r => !r.IsClosed);
        }
    }
}