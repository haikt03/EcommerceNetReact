using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Book
{
    public class UpdateBookDto : CreateBookDto
    {
        [Required(ErrorMessage = "Giá trị này không được để trống")]
        public int Id { get; set; }
    }
}
