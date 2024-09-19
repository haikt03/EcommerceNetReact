namespace Core.Entities
{
    public class Book : BaseEntity
    {
        public required string Title { get; set; }
        public required string Publisher { get; set; }
        public required int PublishedYear { get; set; }
        public required string Language { get; set; }
        public string? Translator { get; set; }
        public required string ISBN { get; set; }
        public required string Description { get; set; }
        public int Price { get; set; }
        public int QuantityInStock { get; set; }

        public List<Image> Images { get; set; } = new List<Image>();

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        public int? AuthorId { get; set; }
        public Author? Author { get; set; }
    }
}
