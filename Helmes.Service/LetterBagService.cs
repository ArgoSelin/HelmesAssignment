using Helmes.Domain.Entities;
using Helmes.Domain.Interfaces;
using Helmes.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Helmes.Service
{
    public class LetterBagService: ILetterBagService
    {
        private readonly IUnitOfWork _unitOfWork;
        public LetterBagService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<LetterBag>> GetAll()
        {
            return await _unitOfWork.Repository<LetterBag>().GetAllAsync();
        }

        public async Task<LetterBag> GetOne(int id)
        {
            return await _unitOfWork.Repository<LetterBag>().FindAsync(id);
        }

        public async Task<List<LetterBag>> GetShipmentLetterBagsByShipmentIdService(int shipmentId)
        {
            return await _unitOfWork.LetterBagRepository.GetShipmentLetterBagsByShipmentId(shipmentId);
        }

        public async Task Update(LetterBag LetterBag)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var LetterBagRepos = _unitOfWork.Repository<LetterBag>();
                var LetterBagResult = await LetterBagRepos.FindAsync(LetterBag.Id);
                if (LetterBagResult == null)
                    throw new KeyNotFoundException();

                LetterBagResult = LetterBag;

                await _unitOfWork.CommitTransaction();
            }
            catch
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task Add(LetterBag LetterBag)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var LetterBagRepos = _unitOfWork.Repository<LetterBag>();
                var hasSameBagNumber = await _unitOfWork.ParcelBagRepository.CheckIfBagNumberIsTaken(LetterBag.BagNumber);
                if(hasSameBagNumber != null)
                {
                    await _unitOfWork.RollbackTransaction();
                    throw new ArgumentNullException(paramName: nameof(LetterBag.BagNumber), message: "Bag number is allready in database!");
                }
                else
                {
                    await LetterBagRepos.InsertAsync(LetterBag);
                    await _unitOfWork.CommitTransaction();
                }
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

                var LetterBagRepos = _unitOfWork.Repository<LetterBag>();
                var LetterBagResult = await LetterBagRepos.FindAsync(id);
                if (LetterBagResult == null)
                    throw new KeyNotFoundException();

                await LetterBagRepos.DeleteAsync(LetterBagResult);

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
