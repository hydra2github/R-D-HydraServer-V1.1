using Microsoft.Extensions.Logging;
using Ocuco.Hydra.WebMVC.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocuco.Hydra.WebMVC.Data
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

        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                logger.LogInformation("GetAllProducts call");

                return ctx.Products
                          .OrderBy(p => p.Title)
                          .ToList();

            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex}");
                return null;
            }            
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return ctx.Products
                      .Where(p => p.Category == category)
                      .ToList();
        }

        public bool SaveAll()
        {
            return ctx.SaveChanges() > 0;
        }

    }
}
