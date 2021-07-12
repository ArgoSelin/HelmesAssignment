using Helmes.Domain.Entities;
using Helmes.Domain.Interfaces.IModelRepositories;
using Microsoft.EntityFrameworkCore;

namespace Helmes.Infrastructure.Repositories
{
    public class FinalBagRepository : Repository<FinalBag>, IFinalBagRepository
    {
        public FinalBagRepository(DbContext context)
        : base(context)
        {
        }

    }
}
