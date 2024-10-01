using Core.Entities;
using System.Reflection;
using System.Text.Json;

namespace Infrastructure.Data.Seed
{
    public class AppSeedData
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (!context.Authors.Any())
            {
                var authorsData = await File
                    .ReadAllTextAsync(path + @"/Data/SeedData/authors.json");

                var authors = JsonSerializer.Deserialize<List<Author>>(authorsData);
                if (authors == null) return;

                context.Authors.AddRange(authors);
                await context.SaveChangesAsync();
            }
        }
    }
}
