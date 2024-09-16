namespace API.DTOs.Books
{
    public class CreateBookDto : BaseBookDto
    {
        public int? CategoryId { get; set; }
        public int? AuthorId { get; set; }
        public List<int> ImageIds { get; set; } = new List<int>();
    }
}