namespace Core.Entities
{
    public class Author : BaseEntity
    {
        public string FullName { get; set; } = string.Empty;
        public string Biography { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        public Image? Image { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();
    }
}
