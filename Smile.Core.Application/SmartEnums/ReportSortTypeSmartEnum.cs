using System.Linq;
using Ardalis.SmartEnum;
using Smile.Core.Common.Enums;
using Smile.Core.Domain.Entities.Support;

namespace Smile.Core.Application.SmartEnums
{
    public abstract class ReportSortTypeSmartEnum : SmartEnum<ReportSortTypeSmartEnum>
    {
        protected ReportSortTypeSmartEnum(string name, int value) : base(name, value) { }

        public static readonly ReportSortTypeSmartEnum Descending = new DescendingType();
        public static readonly ReportSortTypeSmartEnum Ascending = new AscendingType();

        public abstract IQueryable<Report> Sort(IQueryable<Report> reports);

        private sealed class DescendingType : ReportSortTypeSmartEnum
        {
            public DescendingType() : base(nameof(Descending), (int)SortType.Descending) { }

            public override IQueryable<Report> Sort(IQueryable<Report> reports)
                => reports.OrderByDescending(r => r.DateUpdated);
        }

        private sealed class AscendingType : ReportSortTypeSmartEnum
        {
            public AscendingType() : base(nameof(Ascending), (int)SortType.Ascending) { }

            public override IQueryable<Report> Sort(IQueryable<Report> reports)
                => reports.OrderBy(r => r.DateUpdated);
        }
    }
}