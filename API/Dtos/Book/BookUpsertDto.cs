using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Book
{
    public class BookUpsertDto
    {
        private const string RequiredErrorMessage = "Giá trị này không được để trống";
        private const string RangeErrorMessage = "Giá trị này phải lớn hơn hoặc bằng {1}";

        [Required(ErrorMessage = RequiredErrorMessage)]
        public string Name { get; set; } = string.Empty;

        public string? Translator { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        public string Publisher { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(1000, 9999, ErrorMessage = "Năm xuất bản phải hợp lệ")]
        public int PublishedYear { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        public string Language { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(0, int.MaxValue, ErrorMessage = RangeErrorMessage)]
        public int Weight { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(0, int.MaxValue, ErrorMessage = RangeErrorMessage)]
        public int NumberOfPages { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        public string Form { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(0, int.MaxValue, ErrorMessage = RangeErrorMessage)]
        public int Price { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(0, int.MaxValue, ErrorMessage = RangeErrorMessage)]
        public int QuantityInStock { get; set; }

        public int? CategoryId { get; set; }
        public int? AuthorId { get; set; }

        public IFormFile? File { get; set; }
    }
}
