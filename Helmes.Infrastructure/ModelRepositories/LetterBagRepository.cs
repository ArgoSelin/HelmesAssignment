using Helmes.Domain.Entities;
using Helmes.Domain.Interfaces.IModelRepositories;
using Helmes.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helmes.Infrastructure.Repositories
{
    public class LetterBagRepository : Repository<LetterBag>, ILetterBagRepository
    {
        public LetterBagRepository(DbContext context)
        : base(context)
        {
        }

        public async Task<LetterBag> CheckIfBagNumberIsTaken(string bagNumber)
        {
            return await GetAll().FirstOrDefaultAsync(a => a.BagNumber == bagNumber);                     
        }

        public async Task<List<LetterBag>> GetShipmentLetterBagsByShipmentId(int shipmentId)
        {
            return await GetAll().Where(a => a.ShipmentId == shipmentId).ToListAsync();
        }
    }
}
