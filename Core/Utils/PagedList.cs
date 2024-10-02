using Core.Helpers;

namespace Core.Utils
{
    public class PagedList<T> : List<T>
    {
        public PaginationHeader PaginationHeader { get; set; }

        public PagedList(List<T> items, int totalCount, int pageSize, int pageIndex)
        {
            PaginationHeader = new PaginationHeader
            {
                PageSize = pageSize,
                PageIndex = pageIndex,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
            AddRange(items);
        }
    }
}
