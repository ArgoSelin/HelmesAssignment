using Helmes.Domain.Entities;
using Helmes.Domain.Interfaces.IModelRepositories;
using Helmes.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helmes.Infrastructure.Repositories
{
    public class ParcelBagRepository : Repository<ParcelBag>, IParcelBagRepository
    {
        public ParcelBagRepository(DbContext context)
        : base(context)
        {
        }

        public async Task<ParcelBag> CheckIfBagNumberIsTaken(string bagNumber)
        {
            return await GetAll().FirstOrDefaultAsync(a => a.BagNumber == bagNumber);            
        }

        public async Task<ParcelBag> GetParcelBagByBagNumber(string bagNumber)
        {
            return await GetAll().FirstOrDefaultAsync(a => a.BagNumber == bagNumber);
        }

        public async Task<List<ParcelBag>> GetShipmentParcelBagsByShipmentId(int shipmentId)
        {
            return await GetAll().Include(a=>a.ParcelList).Where(a=>a.ShipmentId == shipmentId).ToListAsync();
        }
    }
}
