namespace API.DTOs.Books
{
    public class UpdateBookDto : BaseBookDto
    {
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public int? AuthorId { get; set; }
        public List<int>? ImageIds { get; set; }
    }
}
