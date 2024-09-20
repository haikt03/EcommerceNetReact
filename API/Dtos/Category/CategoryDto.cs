using API.Dtos.Book;

namespace API.Dtos.Category
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? PCategoryName { get; set; }
        public List<string>? CCategorieNames { get; set; }
        public List<BookDto>? Books { get; set; }
    }
}
