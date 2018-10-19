using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocuco.Hydra.WebMVC.Data.Entities
{
    public class ArtOrderItem
    {
        public int Id { get; set; }
        public ArtProduct Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public ArtOrder Order { get; set; }
    }
}
