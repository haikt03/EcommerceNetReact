using Core.Entities;

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

        public static IQueryable<Category> GetHierarchicalCategories(this IQueryable<Category> categories, int? parentId = null)
        {
            return categories
                .Where(c => c.PId == parentId)
                .Select(c => new Category
                {
                    Id = c.Id,
                    Name = c.Name,
                    PId = c.PId,
                    PCategory = c.PCategory,
                    CreatedAt = c.CreatedAt,
                    ModifiedAt = c.ModifiedAt,
                    CCategories = categories.GetHierarchicalCategories(c.Id).ToList(),
                    Books = c.Books
                });
        }
    }
}
