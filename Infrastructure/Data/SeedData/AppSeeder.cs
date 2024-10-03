using Core.Entities;
using System.Reflection;
using System.Text.Json;

namespace Infrastructure.Data.SeedData
{
    public class AppSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            var path = @"D:/Workspace/Personal/NetReact/EcommerceNetReact/Infrastructure/Data/SeedData/";

            if (!context.Categories.Any())
            {
                var categoriesData = await File.ReadAllTextAsync(path + "categories.json");
                var categories = JsonSerializer.Deserialize<List<Category>>(categoriesData);
                if (categories == null)
                    return;

                foreach (var category in categories)
                {
                    context.Categories.Add(category);
                }
                await context.SaveChangesAsync();
            }

            if (!context.Authors.Any())
            {
                var authorsData = await File.ReadAllTextAsync(path + "authors.json");
                var authors = JsonSerializer.Deserialize<List<Author>>(authorsData);
                if (authors == null)
                    return;

                context.Authors.AddRange(authors);
                await context.SaveChangesAsync();
            }
        }
    }
}
