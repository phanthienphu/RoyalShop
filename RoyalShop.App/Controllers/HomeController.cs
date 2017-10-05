﻿using AutoMapper;
using RoyalShop.App.Models;
using RoyalShop.Model.Models;
using RoyalShop.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoyalShop.App.Controllers
{
    public class HomeController : Controller
    {
        IProductCategoryService _productCategoryService;
        ICommonService _commonService;

        public HomeController(IProductCategoryService productCategoryService,ICommonService commonService)
        {
            _productCategoryService = productCategoryService;
            _commonService = commonService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [ChildActionOnly] //chỉ được nhúng, không thể gọi trực tiếp như home/footer
        public ActionResult Footer()
        {
            var model = _commonService.GetFooter();
            var footerViewModel = Mapper.Map<Footer, FooterViewModel>(model);
            return PartialView(footerViewModel);
        }

        [ChildActionOnly] 
        public ActionResult Header()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Category()
        {
            var model = _productCategoryService.GetAll();
            var listProductCategoryViewModel = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);
            return PartialView(listProductCategoryViewModel);
        }
    }
}