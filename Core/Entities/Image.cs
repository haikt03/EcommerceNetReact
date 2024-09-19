namespace Core.Entities
{
    public class Image: BaseEntity
    {
        public required string PublicId { get; set; }
        public required string Url { get; set; }

        public int? BookId { get; set; }
        public Book? Book { get; set; }

        public int? AuthorId { get; set; }
        public Author? Author { get; set; }
    }
}
