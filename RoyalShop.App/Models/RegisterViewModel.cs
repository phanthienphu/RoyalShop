using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoyalShop.App.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Bạn phải nhập tên!")]
        public string FullName { set; get; }

        [Required(ErrorMessage = "Bạn phải nhập tên đăng nhập!")]
        public string UserName { set; get; }

        [Required(ErrorMessage = "Bạn phải nhập mật khẩu!")]
        [MinLength(8,ErrorMessage = "Mật khẩu phải có ít nhất 8 ký tự")]
        public string Password { set; get; }

        [Required(ErrorMessage = "Bạn phải nhập Email!")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không đúng!")]
        public string Email { set; get; }
        public string Address { set; get; }

        [Required(ErrorMessage = "Bạn phải nhập Số điện thoại!")]
        public string PhoneNumber { set; get; }
    }
}