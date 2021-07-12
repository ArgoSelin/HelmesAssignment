using System.Collections.Generic;
using System.Threading.Tasks;
using Helmes.Domain.Entities;

namespace Helmes.Domain.Interfaces.Services
{
    public interface IShipmentService 
    {
        Task<IList<Shipment>> GetAll();
        Task<List<Shipment>> GetAllIncludingBagsService();
        Task<Shipment> GetOne(int id);
        Task<Shipment> GetShipmentByShipmentNumberService(string shipmentNumber);
        Task Update(Shipment shipment);
        Task Add(Shipment shipment);
        Task Delete(int id);
    }
}
