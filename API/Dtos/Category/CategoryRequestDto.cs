using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Category
{
    public class CategoryRequestDto
    {
        [Required(ErrorMessage = "Giá trị này không được để trống")]
        public string Name { get; set; } = string.Empty;
        public int? PId { get; set; }
    }
}
