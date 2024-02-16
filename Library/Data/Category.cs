using System.ComponentModel.DataAnnotations;
using static Library.Data.DataConstants;

namespace Library.Data
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(MaxCategoryName)]
        public string Name { get; set; }  = string.Empty;
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}