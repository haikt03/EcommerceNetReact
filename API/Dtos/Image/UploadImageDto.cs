using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Image
{
    public class UploadImageDto
    {
        public byte[] FileData { get; set; } = Array.Empty<byte>();
        [Required(ErrorMessage = "Giá trị này không được để trống")]
        public string FileName { get; set; } = string.Empty;
    }
}
