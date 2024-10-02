using API.Dtos.Author;
using API.Dtos.Category;
using Core.Entities;
using System.Reflection;
using System.Text.Json;

namespace API.Seeder
{
    public class AppSeeder
    {
        private readonly HttpClient _httpClient;
        public AppSeeder(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task SeedAsync()
        {
            var authorsResponse = await _httpClient.GetFromJsonAsync<List<AuthorDto>>("http://localhost:5000/api/authors");
            var categoriesResponse = await _httpClient.GetFromJsonAsync<List<CategoryDto>>("http://localhost:5000/api/categories");

            if (authorsResponse?.Count > 0 || categoriesResponse?.Count > 0)
                return;

            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var authorsData = await File.ReadAllTextAsync(path + @"/Data/SeedData/authors.json");
            var categoriesData = await File.ReadAllTextAsync(path + @"/Data/SeedData/categories.json");

            if (authorsData == null || categoriesData == null)
                return;

            var authors = JsonSerializer.Deserialize<List<AuthorDto>>(authorsData);
            var categories = JsonSerializer.Deserialize<List<CategoryDto>>(categoriesData);

            if (authors == null || categories == null)
                return;

            foreach (var author in authors)
            {

            }
        }
    }
}
