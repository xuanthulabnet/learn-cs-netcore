using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ef03.Model;

namespace ef03.Model
{
    public class User
    {
        public int UserId {set; get;}

        public string UserName {set; get;}

        public virtual List<Product> ProductsPost {set; get;} 
    }
}