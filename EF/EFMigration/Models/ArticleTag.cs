using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFMigration.Models
{
    [Table("articletag")]
    public class ArticleTag
    {
        [Key]
        public int ArticleTagId {set;  get;}

        public int ArticleId {set; get;}
        [ForeignKey("ArticleId")]
        public Article article {set; get;}

        [StringLength(20)]
        public string TagId {set; get;}
        [ForeignKey("TagId")]
        public Tag tag {set; get;}
    }
}
