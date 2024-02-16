﻿using Humanizer;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Library.Data.DataConstants;
namespace Library.Data
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(MaxTitle)]
        public string Title { get; set; } = string.Empty;
        [Required]
        [StringLength(MaxAuthor)]
        public string Author { get; set; } = string.Empty;
        [Required]
        [StringLength(MaxDescription)]
        public string Description { get; set; } =string.Empty;
        [Required]
        public string ImageUrl { get; set; } = string.Empty;
        [Required]
        [Range(0.00, 10.00)]
        public decimal Rating { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;
        public ICollection<IdentityUserBook> UsersBooks { get; set; } = new List<IdentityUserBook>();
    }
//    • Has Id – a unique integer, Primary Key
//• Has Title – a string with min length 10 and max length 50 (required)
//• Has Author – a string with min length 5 and max length 50 (required)
//• Has Description – a string with min length 5 and max length 5000 (required)
//• Has ImageUrl – a string (required)
//• Has Rating – a decimal with min value 0.00 and max value 10.00 (required)
//• Has CategoryId – an integer, foreign key(required)
//• Has Category – a Category(required)
//• Has UsersBooks – a collection of type IdentityUserBook

}
