using Microsoft.EntityFrameworkCore;

namespace API.Helpers
{
    public class PagedList<T> : List<T>
    {
        public PagedList(List<T> items, int pageSize, int pageIndex)
        {
            MetaData = new MetaData
            {
                PageSize = pageSize,
                PageIndex = pageIndex,
                TotalCount = items.Count(),
                TotalPages = (int)Math.Ceiling(items.Count() / (double)pageSize)
            };
            AddRange(items);
        }

        public MetaData MetaData { get; set; }

        public static async Task<PagedList<T>> ToPagedList(IQueryable<T> query, int pageSize, int pageIndex)
        {
            var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedList<T>(items, pageIndex, pageSize);
        }
    }
}
