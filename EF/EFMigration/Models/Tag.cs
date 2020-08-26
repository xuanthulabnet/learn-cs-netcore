using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFMigration.Models
{
    public class Tag
    {
        [Key]
        [StringLength(20)]
        public string TagId {set; get;}
        [Column(TypeName="ntext")]
        public string Content {set; get;}
    }
}