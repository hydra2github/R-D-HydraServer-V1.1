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
    //
    // Core 2.1
    //

    [Route("api/[Controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        private readonly IHydraRepository repository;
        private readonly ILogger<ProductsController> logger;

        public ProductsController(IHydraRepository repository, ILogger<ProductsController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        // .NET Core 2.0

        //[HttpGet]
        //public IEnumerable<Product> Get()
        //{
        //    try
        //    {
        //        return _repository.GetAllProducts();
        //    }
        //    catch(Exception ex)
        //    {
        //        _logger.LogError($"Failed to get products: {ex}");
        //        return null;
        //    }            
        //}

        //[HttpGet]
        //public JsonResult Get()
        //{
        //    try
        //    {
        //        return Json(_repository.GetAllProducts());
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Failed to get products: {ex}");
        //        return Json("Bad request");
        //    }
        //}

        //[HttpGet]
        //public IActionResult Get()
        //{
        //    try
        //    {
        //        return Ok(_repository.GetAllProducts());
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Failed to get products: {ex}");
        //        return BadRequest("Failed to get products");
        //    }
        //}


        // .NET Core 2.1

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<ArtProduct>> Get()
        {
            try
            {
                return Ok(repository.GetAllArtProducts());
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex}");
                return BadRequest("Failure");
            }
        }


    }
}
