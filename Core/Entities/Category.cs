namespace Core.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public int? PId { get; set; }
        public Category? PCategory { get; set; }

        public ICollection<Category> CCategories { get; set; } = new List<Category>();

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
