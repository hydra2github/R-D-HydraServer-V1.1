using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ocuco.Hydra.WebMVC.Data;
using Ocuco.Hydra.WebMVC.Data.Entities;
using Ocuco.Hydra.WebMVC21.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocuco.Hydra.WebMVC21.Controllers
{
    //
    // Core 2.0
    //

    [Route("api/[Controller]")]
    public class OrdersController : Controller
    {
        private readonly IHydraRepository repository;
        private readonly ILogger<OrdersController> logger;
        private readonly IMapper mapper;

        public OrdersController(IHydraRepository repository, 
                                ILogger<OrdersController> logger,
                                IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get(bool includeItems = true)
        {
            try
            {
                //return Ok(repository.GetAllOrders());
                //return Ok(mapper.Map<IEnumerable<ArtOrder>, IEnumerable<ArtOrderViewModel>>(repository.GetAllOrders()));

                var results = repository.GetAllOrders(includeItems);

                return Ok(mapper.Map<IEnumerable<ArtOrder>, IEnumerable<ArtOrderViewModel>>(results));
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get orders: {ex}");
                return BadRequest("Failed to get orders");
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var order = repository.GetOrderById(id);

                //if (order != null) return Ok(order);
                //else return NotFound();

                if (order != null) return Ok(mapper.Map<ArtOrder, ArtOrderViewModel>(order));
                else return NotFound();

            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get orders: {ex}");
                return BadRequest("Failed to get orders");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]ArtOrderViewModel model)
        {
            // Post(ArtOrder model) from querystring
            // return Ok();

            try
            {
                if (ModelState.IsValid)
                {
                    //var newArtOrder = new ArtOrder()
                    //{
                    //    OrderDate = model.OrderDate,
                    //    OrderNumber = model.OrderNumber,
                    //    Id = model.OrderId
                    //};
                    var newArtOrder = mapper.Map<ArtOrderViewModel, ArtOrder>(model);

                    if (newArtOrder.OrderDate == DateTime.MinValue)
                    {
                        newArtOrder.OrderDate = DateTime.Now;
                    }

                    repository.AddEntity(newArtOrder);
                    if (repository.SaveAll())
                    {
                        //var vm = new ArtOrderViewModel()
                        //{
                        //    OrderId = newArtOrder.Id,
                        //    OrderDate = newArtOrder.OrderDate,
                        //    OrderNumber = newArtOrder.OrderNumber
                        //};
                        //return Created($"/api/orders/{vm.OrderId}", vm);

                        return Created($"/api/orders/{newArtOrder.Id}", mapper.Map<ArtOrder, ArtOrderViewModel>(newArtOrder));
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }                    
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to save new order: {ex}");
            }
            return BadRequest("Failed to save new order");
        }
    }
}
