using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoyalShop.App.Models
{
    public class ContactDetailViewModel
    {
        public int ID { set; get; }

        [Required(ErrorMessage = "Tên không được bỏ trống!")]
        public string Name { set; get; }

        [MaxLength(50, ErrorMessage = "Số điện thoại không vượt quá 50 ký tự!")]
        public string Phone { set; get; }

        [MaxLength(250, ErrorMessage = "Email không vượt quá 50 ký tự!")]
        public string Email { set; get; }

        [MaxLength(250, ErrorMessage = "Websit không vượt quá 50 ký tự!")]
        public string Website { set; get; }

        [MaxLength(250, ErrorMessage = "Địa chỉ không vượt quá 50 ký tự!")]
        public string Address { set; get; }

        public string Other { set; get; }

        public double? Lat { set; get; } //Vĩ độ

        public double? Ing { set; get; } //kinh độ
        public bool Status { set; get; }
    }
}