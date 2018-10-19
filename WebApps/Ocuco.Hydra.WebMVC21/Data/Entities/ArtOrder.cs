using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocuco.Hydra.WebMVC.Data.Entities
{
    public class ArtOrder
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderNumber { get; set; }
        public ICollection<ArtOrderItem> Items { get; set; }

        //public StoreUser User { get; set; }
    }
}
