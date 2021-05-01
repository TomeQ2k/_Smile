namespace Smile.Core.Application.Models.Pagination
{
    public class PaginationHeader
    {
        public int CurrentPage { get; }
        public int ItemsPerPage { get; }
        public int TotalItems { get; }
        public int TotalPages { get; }

        public PaginationHeader(int currentPage, int itemsPerPage, int totalItems, int totalPages)
        {
            this.CurrentPage = currentPage;
            this.ItemsPerPage = itemsPerPage;
            this.TotalItems = totalItems;
            this.TotalPages = totalPages;
        }
    }
}