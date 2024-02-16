using System.ComponentModel.DataAnnotations;
using static Library.Data.DataConstants;

namespace Library.Models
{
    public class CategoryViewModel
    {
        
        public int Id { get; set; }
        [Required]
        [StringLength(MaxCategoryName, MinimumLength = MinCategoryName)]
        public string Name { get; set; } = string.Empty;
    }
}
