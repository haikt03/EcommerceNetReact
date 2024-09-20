using API.Dtos.Image;
using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Author
{
    public class CreateAuthorDto
    {
        [Required(ErrorMessage = "Giá trị này không được để trống")]
        public string FullName { get; set; } = string.Empty;
        public string? Biography { get; set; }
        public string? Country { get; set; }
        public UploadImageDto? Image { get; set; }
    }
}
