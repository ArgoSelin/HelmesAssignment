using Helmes.Domain.Interfaces.IModelRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Helmes.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IShipmentRepository ShipmentRepository { get; }
        IParcelBagRepository ParcelBagRepository { get; }
        ILetterBagRepository LetterBagRepository { get; }
        IFinalBagRepository FinalBagRepository { get; }

        DbContext DbContext { get; }

        IRepository<T> Repository<T>() where T : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        Task BeginTransaction();

        Task CommitTransaction();

        Task RollbackTransaction();
    }
}
