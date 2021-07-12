using System.Collections.Generic;
using System.Threading.Tasks;
using Helmes.Domain.Entities;

namespace Helmes.Domain.Interfaces.Services
{
    public interface IParcelBagService
    {
        Task<IList<ParcelBag>> GetAll();
        Task<ParcelBag> GetOne(int id);
        Task<ParcelBag> GetParcelBagByBagNumberService(string bagNumber);
        Task<List<ParcelBag>> GetShipmentParcelBagsByShipmentIdService(int shipmentId);
        Task Update(ParcelBag parcelBag);
        Task Add(ParcelBag parcelBag);
        Task Delete(int id);
    }
}
