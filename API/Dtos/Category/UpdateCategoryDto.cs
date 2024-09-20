using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Category
{
    public class UpdateCategoryDto : CreateCategoryDto
    {
        [Required(ErrorMessage = "Giá trị này không được để trống")]
        public int Id { get; set; }
    }
}
