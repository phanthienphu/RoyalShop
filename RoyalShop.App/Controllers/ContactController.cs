using AutoMapper;
using BotDetect.Web.Mvc;
using RoyalShop.App.Infrastructure.Extensions;
using RoyalShop.App.Models;
using RoyalShop.Common;
using RoyalShop.Model.Models;
using RoyalShop.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace RoyalShop.App.Controllers
{
    public class ContactController : Controller
    {
        IContactDetailService _contactDetailService;
        IFeedbackService _feedbackService;

        public ContactController(IContactDetailService contactDetailService, IFeedbackService feedbackService)
        {
            this._contactDetailService = contactDetailService;
            this._feedbackService = feedbackService;
        }

        // GET: Contact
        public ActionResult Index()
        {
            FeedbackViewModel viewModel = new FeedbackViewModel();
            viewModel.ContactDetail = GetDetail();

            return View(viewModel);
        }

        [HttpPost]
        [CaptchaValidation("CaptchaCode", "ContactCaptcha", "Bậy zồi! lại nào! ")]
        public ActionResult SendFeedback(FeedbackViewModel feedbackViewModel)
        {
            if(ModelState.IsValid)
            {
                Feedback newFeedback = new Feedback();
                newFeedback.UpdateFeedback(feedbackViewModel);
                _feedbackService.Create(newFeedback);
                _feedbackService.Save();

                ViewData["SuccessMsg"] = "gửi phản hồi thành công!";
                
                //StringBuilder builder = new StringBuilder();
                //builder.Append("Thông tin liên hệ.<br />");

                string content = System.IO.File.ReadAllText(Server.MapPath( "/Common/Client/template/ContactTemplate.html"));
                content = content.Replace("{{Name}}",feedbackViewModel.Name);
                content = content.Replace("{{Email}}", feedbackViewModel.Email);
                content = content.Replace("{{Message}}", feedbackViewModel.Message);

                var adminEmail = ConfigHelper.GetByKey("AdminEmail");
                MailHelper.SendMail(adminEmail, "Thông tin liên hệ từ RoyalShop", content);

                feedbackViewModel.Name = "";
                feedbackViewModel.Email = "";
                 feedbackViewModel.Message = "";
            }

            feedbackViewModel.ContactDetail = GetDetail();
            return View("Index",feedbackViewModel);
        }

        private ContactDetailViewModel GetDetail()
        {
            var model = _contactDetailService.GetDefaultContact();
            var contactViewModel = Mapper.Map<ContactDetail, ContactDetailViewModel>(model);

            return contactViewModel;
        }
    }
}