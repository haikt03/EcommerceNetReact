namespace Core.Entities
{
    public class Author : BaseEntity
    {
        public required string FullName { get; set; }
        public string? Biography { get; set; }
        public string? Country { get; set; }

        public Image? Image { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();
    }
}
