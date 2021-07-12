using Helmes.Domain.Entities;
using Helmes.Domain.Interfaces;
using Helmes.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Helmes.Service
{
    public class FinalBagService: IFinalBagService
    {
        private readonly IUnitOfWork _unitOfWork;
        public FinalBagService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<FinalBag>> GetAll()
        {
            return await _unitOfWork.Repository<FinalBag>().GetAllAsync();
        }

        public async Task<FinalBag> GetOne(int id)
        {
            return await _unitOfWork.Repository<FinalBag>().FindAsync(id);
        }

        public async Task Update(FinalBag FinalBag)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var FinalBagRepos = _unitOfWork.Repository<FinalBag>();
                var FinalBagResult = await FinalBagRepos.FindAsync(FinalBag.Id);
                if (FinalBagResult == null)
                    throw new KeyNotFoundException();

                FinalBagResult.LetterBagList = FinalBag.LetterBagList;

                await _unitOfWork.CommitTransaction();
            }
            catch
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task Add(FinalBag FinalBag)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var FinalBagRepos = _unitOfWork.Repository<FinalBag>();
                await FinalBagRepos.InsertAsync(FinalBag);

                await _unitOfWork.CommitTransaction();
            }
            catch 
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var FinalBagRepos = _unitOfWork.Repository<FinalBag>();
                var FinalBagResult = await FinalBagRepos.FindAsync(id);
                if (FinalBagResult == null)
                    throw new KeyNotFoundException();

                await FinalBagRepos.DeleteAsync(FinalBagResult);

                await _unitOfWork.CommitTransaction();
            }
            catch 
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
        }
    }
}
