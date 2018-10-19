using Ocuco.Hydra.WebMVC21.V2.Data.Entities;
using System.Collections.Generic;


namespace Ocuco.Hydra.WebMVC21.V2.Data
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