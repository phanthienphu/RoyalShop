using AutoMapper;
using OfficeOpenXml;
using RoyalShop.App.Infrastructure.Core;
using RoyalShop.App.Infrastructure.Extensions;
using RoyalShop.App.Models;
using RoyalShop.Common;
using RoyalShop.Model.Models;
using RoyalShop.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace RoyalShop.App.API
{
    
    [RoutePrefix("api/product")]
    //[Authorize]
    public class ProductController : ApiControllerBase
    {
        #region Initialize
        private IProductService _productService;

        public ProductController(IErrorService errorService, IProductService productService)
            : base(errorService)
        {
            this._productService = productService;
        }
        #endregion

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _productService.GetAll(keyword);

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);
                var responseData = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(query);

                var paginationSet = new PaginationSet<ProductViewModel>()
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
            Func<HttpResponseMessage> func = () =>
            {
                var model = _productService.GetAll();

                var responseData = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(model);

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            };

            return CreateHttpResponse(request, func);
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _productService.GetById(id);

                var responseData = Mapper.Map<Product, ProductViewModel>(model);

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        [AllowAnonymous] //cho phép post nặc danh ko cần đăng nhập
        public HttpResponseMessage Create(HttpRequestMessage request, ProductViewModel productVM)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);//error 400
                }
                else
                {
                    var newPorduct = new Product();
                    newPorduct.UpdateProduct(productVM);
                    newPorduct.CreatedDate = DateTime.Now;
                    newPorduct.CreatedBy = User.Identity.Name;
                    _productService.Add(newPorduct);
                    _productService.Save();
                    var responseDate = Mapper.Map<Product, ProductViewModel>(newPorduct);
                    response = request.CreateResponse(HttpStatusCode.Created, responseDate);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous] //cho phép post nặc danh ko cần đăng nhập
        public HttpResponseMessage Update(HttpRequestMessage request, ProductViewModel productVM)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);//error 400
                }
                else
                {
                    var dbPorduct = _productService.GetById(productVM.ID);
                    dbPorduct.UpdateProduct(productVM);
                    dbPorduct.UpdatedDate = DateTime.Now;
                    dbPorduct.UpdatedBy = User.Identity.Name;
                    _productService.Update(dbPorduct);
                    _productService.Save();
                    var responseDate = Mapper.Map<Product, ProductViewModel>(dbPorduct);
                    response = request.CreateResponse(HttpStatusCode.OK, responseDate);
                }

                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        [AllowAnonymous] //cho phép post nặc danh ko cần đăng nhập
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);//error 400
                }
                else
                {
                    var oldProduct = _productService.Delete(id);
                    _productService.Save();
                    var responseDate = Mapper.Map<Product, ProductViewModel>(oldProduct);
                    response = request.CreateResponse(HttpStatusCode.Created, responseDate);
                }

                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous] //cho phép post nặc danh ko cần đăng nhập
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedProducts)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);//error 400
                }
                else
                {
                    var listProduct = new JavaScriptSerializer().Deserialize<List<int>>(checkedProducts);
                    foreach (var item in listProduct)
                    {
                        _productService.Delete(item);
                    }

                    _productService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listProduct.Count);
                }

                return response;
            });
        }

        [Route("import")]
        [HttpPost]
        [AllowAnonymous] //cho phép post nặc danh ko cần đăng nhập
        public async Task<HttpResponseMessage> Import()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                Request.CreateErrorResponse(HttpStatusCode.UnsupportedMediaType, "Định dạng không được server hỗ trợ");
            }

            var root = HttpContext.Current.Server.MapPath("~/UploadedFiles/Excels");
            if (!Directory.Exists(root)) //KT thư mục tồn tại chưa? chưa thì tạo 
            {
                Directory.CreateDirectory(root);
            }

            var provider = new MultipartFormDataStreamProvider(root);
            var result = await Request.Content.ReadAsMultipartAsync(provider);
            //do stuff with files if you wish
            if (result.FormData["categoryId"] == null)
            {
                Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bạn chưa chọn danh mục sản phẩm.");
            }

            //Upload files
            int addedCount = 0;
            int categoryId = 0;
            int.TryParse(result.FormData["categoryId"], out categoryId);
            foreach (MultipartFileData fileData in result.FileData)
            {
                if (string.IsNullOrEmpty(fileData.Headers.ContentDisposition.FileName))
                {
                    return Request.CreateResponse(HttpStatusCode.NotAcceptable, "Yêu cầu không đúng định dạng");
                }
                string fileName = fileData.Headers.ContentDisposition.FileName;
                if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                {
                    fileName = fileName.Trim('"');
                }
                if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                {
                    fileName = Path.GetFileName(fileName);
                }

                var fullPath = Path.Combine(root, fileName);
                File.Copy(fileData.LocalFileName, fullPath, true);

                //insert to DB
                var listProduct = this.ReadProductFromExcel(fullPath, categoryId);
                if (listProduct.Count > 0)
                {
                    foreach (var product in listProduct)
                    {
                        _productService.Add(product);
                        addedCount++;
                    }
                    _productService.Save();
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Đã nhập thành công " + addedCount + " sản phẩm thành công.");
        }

        private List<Product> ReadProductFromExcel(string fullPath, int categoryId)
        {
            using (var package = new ExcelPackage(new FileInfo(fullPath)))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets[1];
                List<Product> listProduct = new List<Product>();
                ProductViewModel productViewModel;
                Product product;

                decimal originalPrice = 0;
                decimal price = 0;
                decimal promotionPrice;


                bool status = false;
                bool showHome = false;
                bool isHot = false;
                int warranty;

                for (int i = workSheet.Dimension.Start.Row + 1; i <= workSheet.Dimension.End.Row; i++)
                {
                    productViewModel = new ProductViewModel();
                    product = new Product();

                    productViewModel.Name = workSheet.Cells[i, 1].Value.ToString();
                    productViewModel.Alias = StringHelper.ToUnsignString(productViewModel.Name);
                    productViewModel.Description = workSheet.Cells[i, 2].Value.ToString();

                    if (int.TryParse(workSheet.Cells[i, 3].Value.ToString(), out warranty))
                    {
                        productViewModel.Warranty = warranty;

                    }

                    decimal.TryParse(workSheet.Cells[i, 4].Value.ToString().Replace(",", ""), out originalPrice);
                    productViewModel.OriginalPrice = originalPrice;

                    decimal.TryParse(workSheet.Cells[i, 5].Value.ToString().Replace(",", ""), out price);
                    productViewModel.Price = price;

                    if (decimal.TryParse(workSheet.Cells[i, 6].Value.ToString(), out promotionPrice))
                    {
                        productViewModel.PromotionPrice = promotionPrice;

                    }

                    productViewModel.Content = workSheet.Cells[i, 7].Value.ToString();
                    productViewModel.MetaKeyword = workSheet.Cells[i, 8].Value.ToString();
                    productViewModel.MetaDescription = workSheet.Cells[i, 9].Value.ToString();

                    productViewModel.CategoryID = categoryId;

                    bool.TryParse(workSheet.Cells[i, 10].Value.ToString(), out status);
                    productViewModel.Status = status;

                    bool.TryParse(workSheet.Cells[i, 11].Value.ToString(), out showHome);
                    productViewModel.HomeFlag = showHome;

                    bool.TryParse(workSheet.Cells[i, 12].Value.ToString(), out isHot);
                    productViewModel.HotFlag = isHot;

                    product.UpdateProduct(productViewModel);
                    listProduct.Add(product);
                }
                return listProduct;
            }
        }
    }
}
