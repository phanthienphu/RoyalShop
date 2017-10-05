using RoyalShop.App.Infrastructure.Core;
using RoyalShop.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RoyalShop.App.API
{
    [RoutePrefix("api/home")]
    [Authorize] //bắt buộc đăng nhập
    public class HomeController : ApiControllerBase
    {
        IErrorService _errorService;
        public HomeController(IErrorService errorService) : base(errorService)
        {
            this._errorService = errorService; //kế thừa constructor
        }

        [HttpGet]
        [Route("TestMethod")]
        public string TestMethod()
        {
            return "Hello, Emperor";
        }
    }
}
