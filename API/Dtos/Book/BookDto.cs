using API.Dtos.Image;

namespace API.Dtos.Book
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Translator { get; set; }
        public string Publisher { get; set; } = string.Empty;
        public int PublishedYear { get; set; }
        public string Language { get; set; } = string.Empty;
        public int Weight { get; set; }
        public int NumberOfPages { get; set; }
        public string Form { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Price { get; set; }
        public int Discount { get; set; }
        public int QuantityInStock { get; set; }
        public string? CategoryName { get; set; }
        public string? AuthorName { get; set; }
        public ImageDto? Image { get; set; }
    }
}
