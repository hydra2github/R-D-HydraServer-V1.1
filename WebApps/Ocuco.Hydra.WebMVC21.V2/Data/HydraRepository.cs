using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ocuco.Hydra.WebMVC21.V2.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocuco.Hydra.WebMVC21.V2.Data
{
    public class HydraRepository : IHydraRepository
    {
        private readonly HydraContext ctx;
        private readonly ILogger<HydraRepository> logger;

        public HydraRepository(HydraContext context, ILogger<HydraRepository> logger)
        {
            this.ctx = context;
            this.logger = logger;
        }

        public void AddEntity(object model)
        {
            ctx.Add(model);
        }

        public IEnumerable<ArtProduct> GetAllArtProducts()
        {
            try
            {
                logger.LogInformation("GetAllProducts call");

                return ctx.ArtProducts
                          .OrderBy(p => p.Title)
                          .ToList();

            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex}");
                return null;
            }            
        }

        public IEnumerable<ArtOrder> GetAllOrders(bool includeItems)
        {
            if (includeItems)
            { 
                return ctx.ArtOrders
                      .Include(o => o.Items)
                      .ThenInclude(p => p.Product)
                      .ToList();
            }
            else
            {
                return ctx.ArtOrders
                          .ToList();
            }
        }

        public ArtOrder GetOrderById(int id)
        {
            //return ctx.ArtOrders.Find(id);

            return ctx.ArtOrders
                      .Include(o => o.Items)
                      .ThenInclude(p => p.Product)
                      .Where(w => w.Id == id)
                      .FirstOrDefault();
        }

        public IEnumerable<ArtProduct> GetProductsByCategory(string category)
        {
            return ctx.ArtProducts
                      .Where(p => p.Category == category)
                      .ToList();
        }

        public bool SaveAll()
        {
            return ctx.SaveChanges() > 0;
        }

    }
}
