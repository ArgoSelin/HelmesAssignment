using Helmes.Domain.Entities;
using Helmes.Domain.Interfaces;
using Helmes.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Helmes.Service
{
    public class ParcelBagService: IParcelBagService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ParcelBagService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<ParcelBag>> GetAll()
        {
            return await _unitOfWork.Repository<ParcelBag>().GetAllAsync();
        }

        public async Task<ParcelBag> GetOne(int id)
        {
            return await _unitOfWork.Repository<ParcelBag>().FindAsync(id);
        }

        public async Task<ParcelBag> GetParcelBagByBagNumberService(string bagNumber)
        {
            return await _unitOfWork.ParcelBagRepository.GetParcelBagByBagNumber(bagNumber);
        }

        public async Task<List<ParcelBag>> GetShipmentParcelBagsByShipmentIdService(int shipmentId)
        {
            return await _unitOfWork.ParcelBagRepository.GetShipmentParcelBagsByShipmentId(shipmentId);
        }

        public async Task Update(ParcelBag ParcelBag)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var ParcelBagRepos = _unitOfWork.Repository<ParcelBag>();
                var ParcelBagResult = await ParcelBagRepos.FindAsync(ParcelBag.Id);
                if (ParcelBagResult == null)
                    throw new KeyNotFoundException();

                ParcelBagResult = ParcelBag;

                await _unitOfWork.CommitTransaction();
            }
            catch
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task Add(ParcelBag ParcelBag)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var ParcelBagRepos = _unitOfWork.Repository<ParcelBag>();
                var hasSameBagNumber = await _unitOfWork.LetterBagRepository.CheckIfBagNumberIsTaken(ParcelBag.BagNumber);
                if (hasSameBagNumber != null)
                {
                    await _unitOfWork.RollbackTransaction();
                    throw new ArgumentNullException(paramName: nameof(LetterBag.BagNumber), message: "Bag number is allready in database!");
                }
                else
                {
                    await ParcelBagRepos.InsertAsync(ParcelBag);
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

                var ParcelBagRepos = _unitOfWork.Repository<ParcelBag>();
                var ParcelBagResult = await ParcelBagRepos.FindAsync(id);
                if (ParcelBagResult == null)
                    throw new KeyNotFoundException();

                await ParcelBagRepos.DeleteAsync(ParcelBagResult);

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
