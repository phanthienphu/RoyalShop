using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoyalShop.App.Models
{
    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { set; get; }
    }
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name="Email")]
        public string Email { set; get; }
    }
}