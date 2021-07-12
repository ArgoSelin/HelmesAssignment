using Helmes.Domain.Entities;
using Helmes.Domain.Interfaces;
using Helmes.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Helmes.Service
{
    public class ParcelService: IParcelService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ParcelService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<Parcel>> GetAll()
        {
            return await _unitOfWork.Repository<Parcel>().GetAllAsync();
        }

        public async Task<Parcel> GetOne(int id)
        {
            return await _unitOfWork.Repository<Parcel>().FindAsync(id);
        }

        public async Task Update(Parcel Parcel)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var ParcelRepos = _unitOfWork.Repository<Parcel>();
                var ParcelResult = await ParcelRepos.FindAsync(Parcel.Id);
                if (ParcelResult == null)
                    throw new KeyNotFoundException();

                ParcelResult = Parcel;

                  await _unitOfWork.CommitTransaction();
            }
            catch
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task Add(Parcel Parcel)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var ParcelRepos = _unitOfWork.Repository<Parcel>();

                await ParcelRepos.InsertAsync(Parcel);

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

                var ParcelRepos = _unitOfWork.Repository<Parcel>();
                var ParcelResult = await ParcelRepos.FindAsync(id);
                if (ParcelResult == null)
                    throw new KeyNotFoundException();

                await ParcelRepos.DeleteAsync(ParcelResult);

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
