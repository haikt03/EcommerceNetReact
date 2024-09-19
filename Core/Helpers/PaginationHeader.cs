namespace Core.Helpers
{
    public class PaginationHeader
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}
