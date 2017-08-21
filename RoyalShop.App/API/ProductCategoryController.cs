using AutoMapper;
using RoyalShop.App.Infrastructure.Core;
using RoyalShop.App.Infrastructure.Extensions;
using RoyalShop.App.Models;
using RoyalShop.Model.Models;
using RoyalShop.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RoyalShop.App.API
{
    [RoutePrefix("api/productcategory")]
    public class ProductCategoryController : ApiControllerBase
    {
        private IProductCategoryService _productCategoryService;

        public ProductCategoryController(IErrorService errorService, IProductCategoryService productCategoryService)
            : base(errorService)
        {
            this._productCategoryService = productCategoryService;
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request,string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
             {
                 int totalRow = 0;
                 var model = _productCategoryService.GetAll(keyword);

                 totalRow = model.Count();
                 var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);
                 var responseData = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(query);

                 var paginationSet = new PaginationSet<ProductCategoryViewModel>()
                 {
                     Items = responseData,
                     Page = page,
                     TotalCount = totalRow,
                     TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                 };
                 var response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                 return response;
             });
        }

        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _productCategoryService.GetAll();

                var responseData = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("Create")]
        [HttpPost]
        [AllowAnonymous] //cho phép post nặc danh ko cần đăng nhập
        public HttpResponseMessage Create(HttpRequestMessage request, ProductCategoryViewModel productCategoryVM)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if(!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);//error 400
                }
                else
                {
                    var newPorductCategory = new ProductCategory();
                    newPorductCategory.UpdateProductCategory(productCategoryVM);
                    _productCategoryService.Add(newPorductCategory);
                    _productCategoryService.Save();
                    var responseDate = Mapper.Map<ProductCategory, ProductCategoryViewModel>(newPorductCategory);
                    response = request.CreateResponse(HttpStatusCode.Created, responseDate);
                }

                return response;
            });
        }
    }
}