using Helmes.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Helmes.Domain.Interfaces.IModelRepositories
{
    public interface IShipmentRepository : IRepository<Shipment>
    {
        Task<Shipment> GetShipmentByShipmentNumber(string shipmentNumber);
        Task<List<Shipment>> GetAllIncludingBags();
    }
}
