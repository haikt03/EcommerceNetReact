using Core.Entities;
using Core.Helpers;

namespace Infrastructure.Extensions
{
    public static class CategoryExtension
    {
        public static IQueryable<Category> Search(this IQueryable<Category> query, string? search = null)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return query;
            }
            var lowerCaseSearch = search.Trim().ToLower();
            var result = query.Where(b => b.Name.ToLower().Contains(lowerCaseSearch));

            return result;
        }
    }
}
