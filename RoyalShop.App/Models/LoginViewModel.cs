using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoyalShop.App.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Bạn phải nhập tải khoản!")]
        public string UserName { set; get; }

        [Required(ErrorMessage = "Bạn phải nhập mật khẩu!")]
        [DataType(DataType.Password)]

        public string Password { set; get; }

        public bool RememberMe { set; get; }
    }
}