using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Author
{
    public class UpdateAuthorDto : CreateAuthorDto
    {
        [Required(ErrorMessage = "Giá trị này không được để trống")]
        public int Id { get; set; }
    }
}
