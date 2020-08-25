using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ef03.Model
{
    [Table("Products")]                          // Ánh xạ bảng Product 
    public class Product
    {
        [Key]                                       // Là Primary key
        public int ProductId {set; get;}

        [Required]                                  // Cột trong DB, Not Null  
        [StringLength(50)]                          // nvarchar(50)
        public string Name {set; get;}

        [Column(TypeName="Money")]                  // cột kiểu Money trong SQL Server (tương ứng decimal trong Model C#)
        public decimal Price {set; get;}

        // hoặc thêm [Required] khi int?
         public int CategoryId {set; get;}           // Thuộc tính sẽ thiết lập là FK

        [ForeignKey("CategoryId")]
        public virtual Category Category {set; get;}       // Sinh FK (CategoryID ~ Cateogry.CategoryID) ràng buộc đến PK key của Category

        
        public int? CategorySecondId;
        [ForeignKey("CategorySecondId")]
        [InverseProperty("products")]
        public virtual Category SecondCategory {set; get;}


        public int? UserPostId {set;  get;}             // Lưu thông tin người Post sản phẩm
        public virtual User UserPost {set; get;}        // Tham chiếu User

    }
}