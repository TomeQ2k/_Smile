using System;
using Smile.Core.Common.Enums;
using Smile.Core.Domain.Mongo.Repositories.Params;

namespace Smile.Core.Application.Features.Requests.Query.Params
{
    public abstract class LogFiltersParams : PaginationRequest, ILogFiltersParams
    {
        private static DateTime minDateValue = DateTime.Now.AddMonths(-3);
        private static DateTime maxDateValue = DateTime.Now;

        private DateTime minDate = minDateValue;

        public DateTime MinDate
        {
            get => minDate;
            set => minDate = (value < minDateValue || value > maxDateValue) ? minDateValue : value;
        }

        private DateTime maxDate = maxDateValue;

        public DateTime MaxDate
        {
            get => maxDate;
            set => maxDate = (value < minDateValue || value > maxDateValue) ? maxDateValue : value;
        }

        public string Level { get; set; }
        public string Message { get; set; }
        public string Url { get; set; }
        public string Action { get; set; }

        public SortType SortType { get; set; } = SortType.Descending;
    }
}