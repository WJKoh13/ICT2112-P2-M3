using ProRental.Domain.Entities;
using ProRental.Interfaces.Data.Module3.P2_5;
using System.Collections.Generic;
using System.Linq;

namespace ProRental.Domain.Module3.P2_5
{
    public class CatalogControl
    {
        private readonly ICatalogQuery _catalogQuery;

        public CatalogControl(ICatalogQuery catalogQuery)
        {
            _catalogQuery = catalogQuery;
        }

        // 🔥 FEATURE 5: Eco Product Discovery
        public List<Catalog> GetEcoProducts()
        {
            var products = _catalogQuery.GetAll();

            return products
                .Where(p => !string.IsNullOrEmpty(p.EcoBadge))
                .OrderBy(p => p.CarbonScore)
                .ToList();
        }

        public List<Catalog> GetByBadge(string badge)
        {
            if (string.IsNullOrWhiteSpace(badge))
            {
                return new List<Catalog>();
            }

            return _catalogQuery.GetByEcoBadge(badge.Trim());
        }

        public List<Catalog> GetSortedByCarbon()
        {
            return _catalogQuery.GetSortedByCarbonScore();
        }
    }
}
