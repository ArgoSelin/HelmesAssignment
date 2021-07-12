using Helmes.Domain.Entities;
using Helmes.Domain.Interfaces.IModelRepositories;
using Helmes.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Helmes.Infrastructure.Repositories
{
    public class ShipmentRepository : Repository<Shipment>, IShipmentRepository
    {
            public ShipmentRepository(DbContext context) 
            : base(context)
            {
            }

            public async Task<Shipment> GetShipmentByShipmentNumber(string shipmentNumber)
            {
                return await GetAll().FirstOrDefaultAsync(a => a.ShipmentNumber == shipmentNumber);
            }

            public async Task<List<Shipment>> GetAllIncludingBags()
            {
                return await GetAll()
                .Include(a=>a.BagList).ThenInclude(a=>a.ParcelBagList).ThenInclude(a=>a.ParcelList)
                .Include(a=>a.BagList).ThenInclude(a=>a.LetterBagList)
                .ToListAsync();
            }
    }
}
