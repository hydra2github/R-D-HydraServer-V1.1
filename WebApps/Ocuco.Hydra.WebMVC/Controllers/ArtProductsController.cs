using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ocuco.Hydra.WebMVC.Data;
using Ocuco.Hydra.WebMVC.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocuco.Hydra.WebMVC.Controllers
{
    [Route("api/[Controller]")]
    public class ArtProductsController : Controller
    {
        private readonly IHydraRepository repository;
        private readonly ILogger<ArtProductsController> logger;

        public ArtProductsController(IHydraRepository repository, ILogger<ArtProductsController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        //[HttpGet]
        //public ActionResult<IEnumerable<Product>> Get()
        //{
        //    try
        //    {
        //        return Ok(repository.GetAllArtProducts());
        //    }
        //    catch (Exception ex )
        //    {
        //        logger.LogError($"Error: {ex}");
        //        return BadRequest("Failure");
        //    }            
        //}


    }
}
