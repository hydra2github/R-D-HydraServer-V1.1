using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ocuco.Hydra.WebMVC21.V2.Data;
using Ocuco.Hydra.WebMVC21.V2.Data.Entities;
using Ocuco.Hydra.WebMVC21.V2.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Ocuco.Hydra.WebMVC21.V2.Controllers.sampleAPI
{
    [Route("/api/orders/{orderid}/items")]
    public class OrderItemsController : Controller
    {
        private readonly IHydraRepository repository;
        private readonly ILogger<OrderItemsController> logger;
        private readonly IMapper mapper;

        public OrderItemsController(IHydraRepository repository, 
                                    ILogger<OrderItemsController> logger,
                                    IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get(int orderId)
        {
            var order = repository.GetOrderById(orderId);
            if (order != null) return Ok(mapper.Map<IEnumerable<ArtOrderItem>, IEnumerable<ArtOrderItemViewModel>>(order.Items));
            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int orderId, int id)
        {
            var order = repository.GetOrderById(orderId);
            if (order != null)
            {
                var item = order.Items.Where(i => i.Id == id).FirstOrDefault();
                if (item != null)
                {
                    return Ok(mapper.Map<ArtOrderItem, ArtOrderItemViewModel>(item));
                }
            }
            return NotFound();
        }
    }
}
