using Microsoft.AspNetCore.Mvc;
using Ocuco.Hydra.WebMVC21.V2.Data;
using Ocuco.Hydra.WebMVC21.V2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocuco.Hydra.WebMVC21.V2.Controllers
{
    //
    // Use this controller for reference only
    //

    [ApiExplorerSettings(IgnoreApi = true)]
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private readonly IHydraRepository repository;

        public AppController(IMailService mailService, IHydraRepository repository)
        {
            this._mailService = mailService;
            this.repository = repository;
        }


        public IActionResult Index()
        {
            //throw new InvalidOperationException("Bad things");

            //var results = ctx.ArtProducts.ToList();
            //var results = repository.GetAllArtProducts();
            //return View(results.ToList());

            return View();
        }

        //[HttpGet("contact")]
        //public IActionResult Contact()
        //{
        //    ViewBag.Title = "Contact Us";

        //    //throw new InvalidOperationException("Bad things");

        //    return View();
        //}

        //[HttpPost("contact")]
        //public IActionResult Contact(ContactViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Send the email
        //        _mailService.SendMessage("daniel.tassi@ocuco.com", model.Subject, $"From: {model.Message}");
        //        ViewBag.UserMessage = "Mail Sent";
        //        ModelState.Clear();
        //    }
        //    else
        //    {
        //        // Show the errors
        //    }

        //    return View();
        //}


        public IActionResult About()
        {
            ViewBag.Title = "About Us";

            return View();
        }



        public IActionResult Shop()
        {
            //var results = context.ArtProducts
            //    .OrderBy(p => p.Category)
            //    .ToList();

            //var results = from p in ctx.ArtProducts
            //              orderby p.Category
            //              select p;

            var results = repository.GetAllArtProducts();

            return View(results.ToList());
        }

    }
}
