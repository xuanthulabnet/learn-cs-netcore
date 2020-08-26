using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ef05.Models
{
    public partial class Products
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int? CategorySecondId { get; set; }
        public int? UserPostId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [InverseProperty("ProductsCategory")]
        public virtual Category Category { get; set; }
        [ForeignKey(nameof(CategorySecondId))]
        [InverseProperty("ProductsCategorySecond")]
        public virtual Category CategorySecond { get; set; }
        [ForeignKey(nameof(UserPostId))]
        [InverseProperty(nameof(User.Products))]
        public virtual User UserPost { get; set; }
    }
}
