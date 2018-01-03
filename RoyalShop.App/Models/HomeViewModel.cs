using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoyalShop.App.Models
{
    public class HomeViewModel
    {
        public IEnumerable<SlideViewModel> slides { set; get; }
        public IEnumerable<ProductViewModel> LastestProducts { set; get; }
        public IEnumerable<ProductViewModel> TopSaleProducts { set; get; }

        public string Title { set; get; }
        public string MetaKeyword { set; get; }
        public string MetaDescription { set; get; }
    }
}