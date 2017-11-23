using AutoMapper;
using Microsoft.AspNet.Identity;
using RoyalShop.App.App_Start;
using RoyalShop.App.Infrastructure.Extensions;
using RoyalShop.App.Models;
using RoyalShop.Common;
using RoyalShop.Model.Models;
using RoyalShop.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace RoyalShop.App.Controllers
{
    public class ShoppingCartController : Controller
    {
        IProductService _productService;
        IOrderService _orderService;
        ApplicationUserManager _userManager;
        public ShoppingCartController(IOrderService orderService,IProductService productService, ApplicationUserManager userManager)
        {
            this._productService = productService;
            this._orderService = orderService;
            this._userManager = userManager;
        }

        // GET: ShoppingCart
        public ActionResult Index()
        {
            if(Session[CommonConstants.SessionCart] == null)
                Session[CommonConstants.SessionCart] = new List<ShoppingCartViewModel>();
            return View();
        }
        public ActionResult CheckOut()
        {
            if (Session[CommonConstants.SessionCart] == null)
            {
                return Redirect("/gio-hang.html");
            }

            return View();
        }
        public JsonResult GetUser()
        {
            if(Request.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var user = _userManager.FindById(userId);
                return Json(new
                {
                    data = user,
                    status = true
                });
            }
            return Json(new { status = false });
            
        }
        public JsonResult CreateOrder(string orderViewModel)
        {
            var order = new JavaScriptSerializer().Deserialize<OrderViewModel>(orderViewModel);
            var orderMap = new Order();
            orderMap.UpdateOrder(order);
            if(Request.IsAuthenticated)
            {
                orderMap.CustomerId = User.Identity.GetUserId();
                orderMap.CreatedBy = User.Identity.GetUserName();
            }
            var cart = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            foreach(var item in cart)
            {
                var detail = new OrderDetail();
                detail.ProductID = item.ProductId;
                detail.Quantity = item.Quantity;
                orderDetails.Add(detail);
            }
            _orderService.Create(orderMap, orderDetails);
            return Json(new { status = true });

        }
        public JsonResult GetAll()
        {
            if (Session[CommonConstants.SessionCart] == null)
                Session[CommonConstants.SessionCart] = new List<ShoppingCartViewModel>();
            var cart = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
            if(cart == null)
            {
                cart = new List<ShoppingCartViewModel>();
            }
            return Json(new
            {
                data = cart,
                status = true
            },JsonRequestBehavior.AllowGet);
        }

        //JsonResult trả về bằng AJAX
        [HttpPost]
        public JsonResult Add(int productId)
        {
            var cart = (List < ShoppingCartViewModel>)Session[CommonConstants.SessionCart] ;
            if(cart.Any(x=>x.ProductId == productId))
            {
                foreach(var item in cart)
                {
                    if(item.ProductId == productId)
                    {
                        item.Quantity += 1;
                    }
                }
            }
            else
            {
                ShoppingCartViewModel newItem = new ShoppingCartViewModel();
                newItem.ProductId = productId;
                var product = _productService.GetById(productId);
                newItem.Product = Mapper.Map<Product, ProductViewModel>(product);
                newItem.Quantity = 1;
                cart.Add(newItem);
            }
            Session[CommonConstants.SessionCart] = cart;

            return Json(new
            {
                status = true
            });
        }

        [HttpPost]
        public JsonResult DeleteItem(int productId)
        {
            var cartSession = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
            if(cartSession != null)
            {
                cartSession.RemoveAll(x => x.ProductId == productId);
                Session[CommonConstants.SessionCart] = cartSession;

                return Json(new
                {
                    status = true
                });
            }
            return Json(new
            {
                status = false
            });
        }

        [HttpPost]
        public JsonResult Update (string cartData)
        {
            var cartViewModel = new JavaScriptSerializer().Deserialize<List<ShoppingCartViewModel>>(cartData); //chuyển string thành list
            var cartSession = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
            foreach(var item in cartSession)
            {
                foreach(var jItem in cartViewModel)
                {
                    if(item.ProductId == jItem.ProductId)
                    {
                        item.Quantity = jItem.Quantity;
                    }
                }
            }
            Session[CommonConstants.SessionCart] = cartSession;

            return Json(new
            {
                status = true
            });
        }

        [HttpPost]
        public JsonResult DeleteAll()
        {
            Session[CommonConstants.SessionCart] = new List<ShoppingCartViewModel>();

            return Json(new
            {
                status = true
            });
        }
    }
}