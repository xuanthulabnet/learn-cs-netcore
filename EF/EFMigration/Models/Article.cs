using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFMigration.Models
{
    [Table("article")]
    public class Article
    {
        [Key]
        public int ArticleId {set; get;}

        [StringLength(100)]
        public string Title {set;  get;}

        // Cột thêm vào khi cập nhật lần 2

        [Column(TypeName="ntext")]
        public string Content {set; get;}


    }
}