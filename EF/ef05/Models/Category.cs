using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ef05.Models
{
    public partial class Category
    {
        public Category()
        {
            ProductsCategory = new HashSet<Products>();
            ProductsCategorySecond = new HashSet<Products>();
        }

        [Key]
        public int CategoryId { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        [InverseProperty(nameof(Products.Category))]
        public virtual ICollection<Products> ProductsCategory { get; set; }
        [InverseProperty(nameof(Products.CategorySecond))]
        public virtual ICollection<Products> ProductsCategorySecond { get; set; }
    }
}
