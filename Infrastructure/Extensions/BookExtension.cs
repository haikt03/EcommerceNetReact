using Core.Entities;

namespace Infrastructure.Extensions
{
    public static class BookExtension
    {
        public static IQueryable<Book> Search(this IQueryable<Book> query, string? search = null)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return query;
            }
            var lowerCaseSearch = search.Trim().ToLower();
            var result = query.Where(b => b.Title.ToLower().Contains(lowerCaseSearch));

            return result;
        }

        public static IQueryable<Book> Filter
        (
            this IQueryable<Book> query,
            string? categories = null,
            string? authors = null,
            string? languages = null,
            int minPrice = 0,
            int maxPrice = 0)
        {
            var categoryList = new List<string>();
            var authorList = new List<string>();
            var languageList = new List<string>();

            if (!string.IsNullOrWhiteSpace(categories))
            {
                categoryList.AddRange(categories.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());
            }
            if (!string.IsNullOrWhiteSpace(authors))
            {
                authorList.AddRange(authors.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());
            }
            if (!string.IsNullOrWhiteSpace(languages))
            {
                languageList.AddRange(languages.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());
            }

            var result = query
                .Where(b => (categoryList.Count == 0 || categoryList.Contains(b.Category.Name)))
                .Where(b => (authorList.Count == 0 || authorList.Contains(b.Author.FullName)))
                .Where(b => (languageList.Count == 0 || languageList.Contains(b.Language)))
                .Where(b => (minPrice == 0 || b.Price >= minPrice))
                .Where(b => (maxPrice == 0 || b.Price <= maxPrice));

            return result;
        }

        public static IQueryable<Book> Sort(this IQueryable<Book> query, string? sort = null)
        {
            if (string.IsNullOrWhiteSpace(sort))
            {
                return query.OrderBy(b => b.Title);
            }

            var result = sort switch
            {
                "price" => query.OrderBy(b => b.Price),
                "priceDesc" => query.OrderByDescending(b => b.Price),
                _ => query.OrderBy(b => b.Title)
            };

            return result;
        }
    }
}
