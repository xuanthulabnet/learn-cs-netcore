using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ef03.Model
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public int CategoryId {set; get;}

        [StringLength(100)]
        public string Name {set; get;}

        [Column(TypeName="ntext")]  
        public string Description {set; get;}

        // Các sản phẩm thuộc về Category   - Đây là điều hướng dạng Collection Navigation (tập hợp)
        public virtual List<Product> products {set; get;}

    }
}