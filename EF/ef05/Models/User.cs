using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ef05.Models
{
    public partial class User
    {
        public User()
        {
            Products = new HashSet<Products>();
        }

        [Key]
        public int UserId { get; set; }
        [Column("user_name")]
        [StringLength(20)]
        public string UserName { get; set; }

        [InverseProperty("UserPost")]
        public virtual ICollection<Products> Products { get; set; }
    }
}
