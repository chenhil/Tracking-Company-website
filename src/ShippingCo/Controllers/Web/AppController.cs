using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ShippingCo.Services;
using ShippingCo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingCo.Controllers.Web
{
    public class AppController : Controller
    {
        private IConfigurationRoot _config;
        private IMailService _mailService;

        public AppController(IMailService mailService, IConfigurationRoot config)
        {
            _mailService = mailService;
            _config = config;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string trackingNumber)
        {
            return RedirectToAction("Tracking", new { tracking = trackingNumber });
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(ContactViewModel contactModel)
        {
            if (ModelState.IsValid)
            {
                var email = _config["Mailsettings:ToAddress"];


                if (string.IsNullOrWhiteSpace(email))
                {
                    ModelState.AddModelError("", "Could not send email, configuration problem.");
                }
                try
                {
                    await _mailService.SendMail(email, contactModel.FirstName, contactModel.LastName, contactModel.Email, contactModel.Message);
                    ModelState.Clear();
                    ViewBag.UserMessage = "Message Sent";
                }
                catch(Exception ex)
                {
                    ViewBag.UserMessage = "Unable to send Email" + ex;
                }
            }
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Tracking()
        {
            var trackingNumber = RouteData.Values["id"];
            if(trackingNumber != null)
            {
                ViewData["Tracking"] = trackingNumber;
            }
            return View();
        }

        [Authorize(Roles="Admin")]
        public IActionResult Manage()
        {
            return View();
        }

    }
}
