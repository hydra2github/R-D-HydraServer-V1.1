using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Ocuco.Hydra.WebMVC21.V2.Data.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocuco.Hydra.WebMVC21.V2.Data
{
    public class HydraSeeder
    {
        private readonly HydraContext ctx;
        private readonly IHostingEnvironment hosting;

        public HydraSeeder(HydraContext ctx, IHostingEnvironment hosting)
        {
            this.ctx = ctx;
            this.hosting = hosting;
        }

        public void Seed()
        {
            ctx.Database.EnsureCreated();

            if (!ctx.ArtProducts.Any())
            {
                // Need to create sample data
                var filepath = Path.Combine(hosting.ContentRootPath, "Data/art.json");
                var json = File.ReadAllText(filepath);
                var products = JsonConvert.DeserializeObject<IEnumerable<ArtProduct>>(json);
                ctx.ArtProducts.AddRange(products);

                var order = new ArtOrder()
                {
                    OrderDate = DateTime.Now,
                    OrderNumber = "00001",
                    //User = user,
                    Items = new List<ArtOrderItem>()
                    {
                        new ArtOrderItem()
                        {
                            Product = products.First(),
                            Quantity = 5,
                            UnitPrice = products.First().Price
                        }
                    }
                };

                ctx.ArtOrders.Add(order);

                ctx.SaveChanges();

                var orderCheck = ctx.ArtOrders.Where(o => o.Id == 1).FirstOrDefault();
                if (orderCheck != null)
                {
                    orderCheck.Items = new List<ArtOrderItem>()
                    {
                        new ArtOrderItem()
                        {
                            Product = products.First(),
                            Quantity = 5,
                            UnitPrice = products.First().Price
                        }
                    };
                }

                ctx.SaveChanges();
            }
        }
    }
}
