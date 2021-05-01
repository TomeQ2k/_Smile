using System.Collections.Generic;
using System.Linq;
using Ardalis.SmartEnum;
using Smile.Core.Application.Models.Mongo;
using Smile.Core.Common.Enums;

namespace Smile.Core.Application.SmartEnums
{
    public abstract class LogSortTypeSmartEnum : SmartEnum<LogSortTypeSmartEnum>
    {
        protected LogSortTypeSmartEnum(string name, int value) : base(name, value) { }

        public static readonly LogSortTypeSmartEnum Descending = new DescendingType();
        public static readonly LogSortTypeSmartEnum Ascending = new AscendingType();

        public abstract IEnumerable<LogDocument> Sort(IEnumerable<LogDocument> logs);

        private sealed class DescendingType : LogSortTypeSmartEnum
        {
            public DescendingType() : base(nameof(Descending), (int)SortType.Descending) { }

            public override IEnumerable<LogDocument> Sort(IEnumerable<LogDocument> logs)
                => logs.OrderByDescending(l => l.Date);
        }

        private sealed class AscendingType : LogSortTypeSmartEnum
        {
            public AscendingType() : base(nameof(Ascending), (int)SortType.Ascending) { }

            public override IEnumerable<LogDocument> Sort(IEnumerable<LogDocument> logs)
                => logs.OrderBy(l => l.Date);
        }
    }
}