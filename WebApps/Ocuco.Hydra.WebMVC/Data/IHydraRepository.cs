using System.Collections.Generic;
using Ocuco.Hydra.WebMVC.Data.Entities;

namespace Ocuco.Hydra.WebMVC.Data
{
    public interface IHydraRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);
        bool SaveAll();
    }
}