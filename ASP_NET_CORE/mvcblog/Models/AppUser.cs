using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace mvcblog.Models {
    public class AppUser : IdentityUser {
        [MaxLength (100)]
        public string FullName { set; get; }

        [MaxLength (255)]
        public string Address { set; get; }

        [DataType (DataType.Date)]
        public DateTime? Birthday { set; get; }

    }
}