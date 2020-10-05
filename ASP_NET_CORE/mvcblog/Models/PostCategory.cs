using System.ComponentModel.DataAnnotations.Schema;

namespace mvcblog.Models
{
    public class PostCategory
    {

        public int PostID {set; get;}

        public int CategoryID {set; get;}

        [ForeignKey("PostID")]
        public Post Post {set; get;}

        [ForeignKey("CategoryID")]
        public Category Category {set; get;}
        
    }
}