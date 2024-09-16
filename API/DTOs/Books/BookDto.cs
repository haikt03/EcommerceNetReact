namespace API.DTOs.Books
{
    public class BookDto
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
        public string CategoryName { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;
        public List<string> ImageUrls { get; set; } = new List<string>();

    }
}
