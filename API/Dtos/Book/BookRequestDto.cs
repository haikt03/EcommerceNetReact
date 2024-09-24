using API.Dtos.Image;
using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Book
{
    public class BookRequestDto
    {
        private const string RequiredErrorMessage = "Giá trị này không được để trống";
        private const string RangeErrorMessage = "Giá trị này phải lớn hơn hoặc bằng {1}";

        [Required(ErrorMessage = RequiredErrorMessage)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        public string Publisher { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(1000, 9999, ErrorMessage = "Năm xuất bản phải hợp lệ")]
        public int PublishedYear { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        public string Language { get; set; } = string.Empty;

        public string? Translator { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [RegularExpression(@"^(?=(?:[^0-9]*[0-9]){10}(?:(?:[^0-9]*[0-9]){3})?$)[\d-]+$", ErrorMessage = "ISBN không hợp lệ")]
        public string ISBN { get; set; } = string.Empty;

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
    }
}
