using System.Collections.Generic;
using Ocuco.Hydra.WebMVC.Data.Entities;

namespace Ocuco.Hydra.WebMVC.Data
{
    public interface IHydraRepository
    {
        IEnumerable<ArtProduct> GetAllArtProducts();
        IEnumerable<ArtProduct> GetProductsByCategory(string category);


        IEnumerable<ArtOrder> GetAllOrders(bool includeItems);
        ArtOrder GetOrderById(int id);


        bool SaveAll();
        void AddEntity(object model);
    }
}