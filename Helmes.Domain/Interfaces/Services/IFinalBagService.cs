using System.Collections.Generic;
using System.Threading.Tasks;
using Helmes.Domain.Entities;

namespace Helmes.Domain.Interfaces.Services
{
    public interface IFinalBagService
    {
        Task<IList<FinalBag>> GetAll();
        Task<FinalBag> GetOne(int id);
        Task Update(FinalBag finalBag);
        Task Add(FinalBag finalBag);
        Task Delete(int id);
    }
}
