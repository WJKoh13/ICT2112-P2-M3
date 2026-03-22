using ProRental.Domain.Entities;
using System.Collections.Generic;

namespace ProRental.Interfaces.Data.Module3.P2_5
{
    public interface ICatalogQuery
    {
        List<Catalog> GetAll();
        List<Catalog> GetByEcoBadge(string badge);
        List<Catalog> GetSortedByCarbonScore();
    }
}