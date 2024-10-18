using Core.Entities;

namespace Infrastructure.Extensions
{
    public static class AppUseExtension
    {
        public static IQueryable<AppUser> Search(this IQueryable<AppUser> query, string? search = null)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return query;
            }
            var lowerCaseSearch = search.Trim().ToLower();
            var result = query.Where(b => b.UserName!.ToLower().Contains(lowerCaseSearch));

            return result;
        }
    }
}
