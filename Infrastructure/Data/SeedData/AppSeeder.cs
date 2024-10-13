using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Infrastructure.Services;
using System.IO;
using System.Reflection;
using System.Text.Json;

namespace Infrastructure.Data.SeedData
{
    public class AppSeeder
    {
        public static async Task SeedAsync(AppDbContext context, ICloudImageService cloudImageService)
        {
            var basePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"../Infrastructure"));

            if (!context.Categories.Any())
            {
                var categoriesData = await File.ReadAllTextAsync(Path.Combine(basePath, @"Data/SeedData/categories.json"));
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
                var authorsData = await File.ReadAllTextAsync(Path.Combine(basePath, @"Data/SeedData/authors.json"));
                var authorsWithUrl = JsonSerializer.Deserialize<List<AuthorWithUrl>>(authorsData);
                if (authorsWithUrl == null)
                    return;

                foreach (var authorWithUrl in authorsWithUrl)
                {
                    var author = new Author
                    {
                        FullName = authorWithUrl.FullName,
                        Biography = authorWithUrl?.Biography,
                        Country = authorWithUrl?.Country
                    };

                    if (authorWithUrl?.ImageUrl != null)
                    {
                        using (var fileStream = File.OpenRead(Path.Combine(basePath, @$"StaticFiles\Images\Authors\{authorWithUrl.ImageUrl}")))
                        {
                            var uploadResult = await cloudImageService.UploadImageAsync(new UploadImageParam { FileStream = fileStream, FileName = authorWithUrl.ImageUrl });
                            if (uploadResult != null)
                                author.Image = new Image
                                {
                                    PublicId = uploadResult.PublicId,
                                    Url = uploadResult.Url
                                };
                        }
                    }
                    context.Authors.Add(author);
                }
                await context.SaveChangesAsync();
            }
        }

        public class AuthorWithUrl : Author
        {
            public string? ImageUrl { get; set; }
        }
    }
}
