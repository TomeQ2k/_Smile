using System;
using Smile.Core.Common.Enums;

namespace Smile.Core.Domain.Mongo.Repositories.Params
{
    public interface ILogFiltersParams
    {
        DateTime MinDate { get; set; }
        DateTime MaxDate { get; set; }

        string Level { get; set; }
        string Message { get; set; }
        string Url { get; set; }
        string Action { get; set; }

        SortType SortType { get; set; }
    }
}