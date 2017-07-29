using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RoyalShop.App.Infrastructure.Core;
using RoyalShop.Model.Models;
using RoyalShop.Service;
using AutoMapper;
using RoyalShop.App.Models;
using RoyalShop.App.Infrastructure.Extensions;

namespace RoyalShop.App.API
{
    [RoutePrefix("api/postcategory")]
    public class PostCategoryController : ApiControllerBase
    {
        private IPostCategoryService _postCategoryService;

        public PostCategoryController(IErrorService errorService, IPostCategoryService postCategoryService) : base(errorService)
        {
            this._postCategoryService = postCategoryService;
        }

        [Route("getall")]
        //Select
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                    var listCategory = _postCategoryService.GetAll();

                var listPostCategoryVM = Mapper.Map<List<PostCategoryViewModel>>(listCategory);

                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listCategory);

                    return response;
            });
        }

        //Create
        [Route("add")]
        public HttpResponseMessage Post(HttpRequestMessage request, PostCategoryViewModel postCategoryVM)
        {
            return CreateHttpResponse(request, () =>
             {
                 HttpResponseMessage response = null;
                 if (ModelState.IsValid)
                 {
                     request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                 }
                 else
                 {
                     PostCategory postCategory = new PostCategory();
                     postCategory.UpdatePostCategory(postCategoryVM);
                     var category = _postCategoryService.Add(postCategory);
                     _postCategoryService.Save();

                     response = request.CreateResponse(HttpStatusCode.Created, category);
                 }
                 return response;
             });
        }

        //Update
        [Route("update")]
        public HttpResponseMessage Put(HttpRequestMessage request, PostCategoryViewModel postCategoryVM)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var postCategoryDb = _postCategoryService.GetById(postCategoryVM.ID);
                    postCategoryDb.UpdatePostCategory(postCategoryVM);
                    _postCategoryService.Update(postCategoryDb);
                    _postCategoryService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK);//HttpStatusCode.OK: lỗi 200
                }
                return response;
            });
        }

        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    _postCategoryService.Delete(id);
                    _postCategoryService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK);//HttpStatusCode.OK: lỗi 200
                }
                return response;
            });
        }
    }
}