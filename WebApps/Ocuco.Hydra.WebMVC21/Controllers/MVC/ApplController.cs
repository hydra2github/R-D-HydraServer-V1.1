using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocuco.Hydra.WebMVC21.Controllers
{
    public class ApplController : Controller
    {
        public ApplController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
