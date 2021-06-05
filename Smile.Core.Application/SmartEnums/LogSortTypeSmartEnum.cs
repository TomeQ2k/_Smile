using Ardalis.SmartEnum;
using MongoDB.Driver.Linq;
using Smile.Core.Common.Enums;
using Smile.Core.Domain.Entities.LogEntity;

namespace Smile.Core.Application.SmartEnums
{
    public abstract class LogSortTypeSmartEnum : SmartEnum<LogSortTypeSmartEnum>
    {
        protected LogSortTypeSmartEnum(string name, int value) : base(name, value)
        {
        }

        public static readonly LogSortTypeSmartEnum Descending = new DescendingType();
        public static readonly LogSortTypeSmartEnum Ascending = new AscendingType();

        public abstract IMongoQueryable<LogDocument> Sort(IMongoQueryable<LogDocument> logs);

        private sealed class DescendingType : LogSortTypeSmartEnum
        {
            public DescendingType() : base(nameof(Descending), (int) SortType.Descending)
            {
            }

            public override IMongoQueryable<LogDocument> Sort(IMongoQueryable<LogDocument> logs)
                => logs.OrderByDescending(l => l.Date);
        }

        private sealed class AscendingType : LogSortTypeSmartEnum
        {
            public AscendingType() : base(nameof(Ascending), (int) SortType.Ascending)
            {
            }

            public override IMongoQueryable<LogDocument> Sort(IMongoQueryable<LogDocument> logs)
                => logs.OrderBy(l => l.Date);
        }
    }
}