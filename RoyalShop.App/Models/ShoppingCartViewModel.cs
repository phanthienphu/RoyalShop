using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoyalShop.App.Models
{
    [Serializable] //chuyển đối tượng này sang session (chuyển các thể hiện class bên dưới sang nhị phân
    public class ShoppingCartViewModel
    {
        public int ProductId { set; get; }
        public ProductViewModel Product { set; get; }
        public int Quantity { set; get; }
    }
}