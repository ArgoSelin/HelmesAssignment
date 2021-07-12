using System.Collections.Generic;
using System.Threading.Tasks;
using Helmes.Domain.Entities;

namespace Helmes.Domain.Interfaces.Services
{
    public interface ILetterBagService
    {
        Task<IList<LetterBag>> GetAll();
        Task<LetterBag> GetOne(int id);
        Task<List<LetterBag>> GetShipmentLetterBagsByShipmentIdService(int shipmentId);
        Task Update(LetterBag letterBag);
        Task Add(LetterBag letterBag);
        Task Delete(int id);
    }
}
