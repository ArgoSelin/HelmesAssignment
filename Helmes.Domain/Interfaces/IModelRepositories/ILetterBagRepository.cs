using Helmes.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Helmes.Domain.Interfaces.IModelRepositories
{
    public interface ILetterBagRepository : IRepository<LetterBag>
    {
        Task<LetterBag> CheckIfBagNumberIsTaken(string bagNumber);
        Task<List<LetterBag>> GetShipmentLetterBagsByShipmentId(int shipmentId);
    }
}
