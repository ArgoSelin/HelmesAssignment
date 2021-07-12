using Helmes.Domain.Entities;
using Helmes.Domain.Interfaces;
using Helmes.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Helmes.Service
{
    public class ShipmentService: IShipmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShipmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<Shipment>> GetAll()
        {
            return await _unitOfWork.Repository<Shipment>().GetAllAsync();
        }

        public async Task<List<Shipment>> GetAllIncludingBagsService()
        {
            return await _unitOfWork.ShipmentRepository.GetAllIncludingBags();
        }

        public async Task<Shipment> GetOne(int id)
        {
            return await _unitOfWork.Repository<Shipment>().FindAsync(id);
        }

        public async Task<Shipment> GetShipmentByShipmentNumberService(string shipmentNumber)
        {
            return await _unitOfWork.ShipmentRepository.GetShipmentByShipmentNumber(shipmentNumber);
        }

        public async Task Update(Shipment Shipment)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var ShipmentRepos = _unitOfWork.Repository<Shipment>();
                var ShipmentResult = await ShipmentRepos.FindAsync(Shipment.Id);
                if (ShipmentResult == null)
                    throw new KeyNotFoundException();

                await _unitOfWork.CommitTransaction();
            }
            catch
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task Add(Shipment Shipment)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var ShipmentRepos = _unitOfWork.Repository<Shipment>();
                await ShipmentRepos.InsertAsync(Shipment);

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

                var ShipmentRepos = _unitOfWork.Repository<Shipment>();
                var ShipmentResult = await ShipmentRepos.FindAsync(id);
                if (ShipmentResult == null)
                    throw new KeyNotFoundException();

                await ShipmentRepos.DeleteAsync(ShipmentResult);

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
