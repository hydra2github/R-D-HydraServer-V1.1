using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ocuco.Hydra.WebMVC21.V2.Services;
using Ocuco.Hydra.WebMVC21.V2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocuco.Hydra.WebMVC21.V2.Controllers
{
    public class ApplController : Controller
    {
        private readonly ILogger<ApplController> logger;
        private readonly IMailService mailService;

        public ApplController(ILogger<ApplController> logger,
                              IMailService mailService)
        {
            this.logger = logger;
            this.mailService = mailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            ViewBag.Title = "Contact Us";

            //throw new InvalidOperationException("Bad things");

            return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Send the email
                mailService.SendMessage("daniel.tassi@ocuco.com", model.Subject, $"From: {model.Message}");
                ViewBag.UserMessage = "Mail Sent";
                ModelState.Clear();
            }
            else
            {
                // Show the errors
            }
            return View();
        }


        [HttpGet("RxoCheckFrame")]
        public IActionResult RxoCheckFrame()
        {
            ViewBag.Title = "Contact Us";

            //throw new InvalidOperationException("Bad things");

            return View();
        }

        [HttpPost("RxoCheckFrame")]
        public IActionResult RxoCheckFrame(CheckFrameViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Send the email
                //mailService.SendMessage("daniel.tassi@ocuco.com", model.Subject, $"From: {model.Message}");
                ViewBag.UserMessage = "Mail Sent";
                ModelState.Clear();
            }
            else
            {
                // Show the errors
            }
            return View();
        }



    }
}
