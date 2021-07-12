using Helmes.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Helmes.Domain.Interfaces.IModelRepositories
{
    public interface IParcelBagRepository : IRepository<ParcelBag>
    {
        Task<ParcelBag> CheckIfBagNumberIsTaken(string bagNumber);
        Task<ParcelBag> GetParcelBagByBagNumber(string bagNumber);
        Task<List<ParcelBag>> GetShipmentParcelBagsByShipmentId(int shipmentId);
    }
}
