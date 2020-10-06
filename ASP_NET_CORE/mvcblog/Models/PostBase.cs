using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvcblog.Models
{
    public class PostBase
    {
        [Key]
        public int PostId {set; get;}

        [Required(ErrorMessage = "Phải có tiêu đề bài viết")]
        [Display(Name = "Tiêu đề")]
        [StringLength(160, MinimumLength = 5, ErrorMessage = "{0} dài {1} đến {2}")]
        public string Title {set; get;}

        [Display(Name = "Mô tả ngắn")]
        public string Description {set; get;}

        [Display(Name="Chuỗi định danh (url)", Prompt = "Nhập hoặc để trống tự phát sinh theo Title")]
        [Required(ErrorMessage = "Phải thiết lập chuỗi URL")]
        [StringLength(160, MinimumLength = 5, ErrorMessage = "{0} dài {1} đến {2}")]
        [RegularExpression(@"^[a-z0-9-]*$", ErrorMessage = "Chỉ dùng các ký tự [a-z0-9-]")]
        public string Slug {set; get;}

        [Display(Name = "Nội dung")]
        public string Content {set; get;}

        [Display(Name = "Xuất bản")]

        public bool Published {set; get;}

        public List<PostCategory>  PostCategories { get; set; }

    }
}