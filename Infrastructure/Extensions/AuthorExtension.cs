using Core.Entities;

namespace Infrastructure.Extensions
{
    public static class AuthorExtension
    {
        public static IQueryable<Author> Search(this IQueryable<Author> query, string? search = null)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return query;
            }
            var lowerCaseSearch = search.Trim().ToLower();
            var result = query.Where(b => b.FullName.ToLower().Contains(lowerCaseSearch));

            return result;
        }
    }
}
