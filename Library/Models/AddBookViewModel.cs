using System.ComponentModel.DataAnnotations;
using static Library.Data.DataConstants;

namespace Library.Models
{
    public class AddBookViewModel
    {
        [Required]
        [StringLength(MaxTitle, MinimumLength = MinTitle)]
        public string Title { get; set; } = string.Empty;
        [Required]
        [StringLength(MaxDescription, MinimumLength = MinDescription)]
        public string Description { get; set; }= string.Empty;
        [Required]
        [StringLength(MaxAuthor, MinimumLength = MinAuthor)]
        public string Author { get; set; } = string.Empty;
        [Required(AllowEmptyStrings = false)]
        public string Url { get; set; } = string.Empty;
        [Required]
        [Range(0.00, 10.00)]
        public decimal Rating { get; set; }
        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }
        public ICollection<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();

    }
}
