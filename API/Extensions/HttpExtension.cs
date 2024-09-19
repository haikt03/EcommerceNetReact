using Core.Helpers;
using System.Text.Json;

namespace API.Extensions
{
    public static class HttpExtension
    {
        public static void AddPaginationHeader(this HttpResponse response, PaginationHeader paginationHeader)
        {
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            response.Headers.Append("Pagination", JsonSerializer.Serialize(paginationHeader, options));
            response.Headers.Append("Access-Control-Expose-Headers", "Pagination");
        }
    }
}
