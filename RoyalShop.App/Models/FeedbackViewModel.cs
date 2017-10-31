using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoyalShop.App.Models
{
    public class FeedbackViewModel
    {
        public int ID { set; get; }

        [MaxLength(250,ErrorMessage = "Chỉ được nhập tới 250 ký tự!")]
        [Required(ErrorMessage = "Không được bỏ trống!")]
        public string Name { set; get; }

        [MaxLength(250, ErrorMessage = "Chỉ được nhập tới 250 ký tự!")]
        public string Email { set; get; }

        [MaxLength(500, ErrorMessage = "Chỉ được nhập tới 500 ký tự!")]
        public string Message { set; get; }

        public DateTime CreatedDate { set; get; }

        [Required(ErrorMessage = "Không được bỏ trống!")]
        public bool Status { set; get; }

        public ContactDetailViewModel ContactDetail { set; get; }
    }
}