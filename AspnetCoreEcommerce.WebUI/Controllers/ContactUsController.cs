using AspnetCoreEcommerce.Core.Domain.Messages;
using AspnetCoreEcommerce.Core.Interface.Messages;
using AspnetCoreEcommerce.WebUI.Models.ContactUsViewModels;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AspnetCoreEcommerce.WebUI.Controllers
{
    public class ContactUsController : Controller
    {
        #region Fields

        private readonly IContactUsService _contactUsService;

        #endregion

        #region Constructor

        public ContactUsController(IContactUsService contactUsService)
        {
            _contactUsService = contactUsService;
        }

        #endregion

        #region Methods

        // GET: /ContactUs/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateMessage(ContactUsViewModel model)
        {
            bool err = true;
            if (ModelState.IsValid)
            {
                var messageEntity = new ContactUsMessage
                {
                    Name = model.Name,
                    Email = model.Email,
                    Title = model.Title,
                    Message = model.Message,
                    Read = false,
                    SendDate = DateTime.Now
                };

                _contactUsService.InsertMessage(messageEntity);
                err = false;
            }

            TempData["ContactUsErr"] = err;
            return RedirectToAction("Index");
        }

        #endregion
    }
}
