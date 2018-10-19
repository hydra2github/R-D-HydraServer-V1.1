using Microsoft.EntityFrameworkCore;
using Ocuco.Hydra.WebMVC21.V2.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocuco.Hydra.WebMVC21.V2.Data
{
    public class HydraContext : DbContext
    {
        public HydraContext(DbContextOptions<HydraContext> options) : base(options)
        {
        }

        public DbSet<ArtProduct> ArtProducts { get; set; }
        public DbSet<ArtOrder> ArtOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // AC 2.1
            //modelBuilder.Entity<Order>()
            //    .HasData(new Order()
            //    {
            //        Id = 1,
            //        OrderDate = DateTime.UtcNow,
            //        OrderNumber = "12345"
            //    });
        }
    }
}
