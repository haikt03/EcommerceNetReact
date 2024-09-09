namespace Core.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public int PublishedYear { get; set; }
        public string Supplier { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public string Translator { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Price { get; set; }
        public int QuantityInStock { get; set; }

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        public int? AuthorId { get; set; }
        public Author? Author { get; set; }
    }
}
