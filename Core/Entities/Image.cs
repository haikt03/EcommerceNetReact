namespace Core.Entities
{
    public class Image: BaseEntity
    {
        public string Url { get; set; } = string.Empty;
        public string PublicId { get; set; } = string.Empty;

        public int? BookId { get; set; }
        public Book? Book { get; set; }

        public int? AuthorId { get; set; }
        public Author? Author { get; set; }
    }
}
