using API.Dtos.Book;
using API.Dtos.Image;

namespace API.Dtos.Author
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? Biography { get; set; }
        public string? Country { get; set; }

        public AuthorImageDto? Image { get; set; }
        public List<BookDto>? Books { get; set; } = new List<BookDto>();
    }
}
