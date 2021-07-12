using System.Collections.Generic;
using System.Threading.Tasks;
using Helmes.Domain.Entities;

namespace Helmes.Domain.Interfaces.Services
{
    public interface IParcelService
    {
        Task<IList<Parcel>> GetAll();
        Task<Parcel> GetOne(int id);
        Task Update(Parcel parcel);
        Task Add(Parcel parcel);
        Task Delete(int id);
    }
}
