using Smile.Core.Common.Helpers;

namespace Smile.Core.Application.Features.Requests.Query
{
    public abstract class PaginationRequest
    {
        protected const int MaxPageSize = int.MaxValue;
        protected const int MinPageNumber = 1;

        protected int pageNumber = MinPageNumber;
        public int PageNumber
        {
            get => pageNumber;
            set => pageNumber = (value < MinPageNumber) ? MinPageNumber : value;
        }

        protected int pageSize = Constants.PageSize;
        public int PageSize
        {
            get => pageSize;
            set => pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}