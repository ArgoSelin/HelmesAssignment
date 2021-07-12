using Helmes.Domain.Entities;
using Helmes.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helmes.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ShipmentController : ControllerBase
    {
        private readonly IShipmentService _ShipmentService;
        private readonly IParcelBagService _ParcelBagService;
        private readonly ILetterBagService _LetterBagService;
        private readonly IFinalBagService _FinalBagService;

        public ShipmentController(IShipmentService ShipmentService, 
            IParcelBagService ParcelBagService, 
            ILetterBagService LetterBagService, 
            IFinalBagService FinalBagService)
        {
            _ShipmentService = ShipmentService;
            _ParcelBagService = ParcelBagService;
            _LetterBagService = LetterBagService;
            _FinalBagService = FinalBagService;
        }

        [HttpPost("CreateShipment")]
        public async Task<Shipment> CreateShipment([FromBody] Shipment Shipment)
        {
            Shipment.CreatedTime = DateTime.UtcNow;
            Shipment.IsFinalized = false;
            await _ShipmentService.Add(Shipment);
            var shipment = await _ShipmentService.GetShipmentByShipmentNumberService(Shipment.ShipmentNumber);

            return shipment;
        }

        [HttpGet("GetAllShipments")]
        public async Task<ActionResult<List<Shipment>>> GetAllShipments()
        {
            var result = await _ShipmentService.GetAllIncludingBagsService();
            return result.ToList();
        }

        [HttpGet("GetShipmentDetails")]
        public async Task<ActionResult<Shipment>> GetShipmentDetails(int id)
        {
            var result = await _ShipmentService.GetOne(id);
            return result;
        }

        [HttpPut("FinalizeShipment")]
        public async Task<IActionResult> FinalizeShipment(int id)
        {
            var shipment = await _ShipmentService.GetOne(id);

            var shipmentParcelBagsListFromDb = await _ParcelBagService.GetShipmentParcelBagsByShipmentIdService(shipment.Id);
            var shipmentLetterBagsListFromDb = await _LetterBagService.GetShipmentLetterBagsByShipmentIdService(shipment.Id);

            for (int i = 0; i < shipmentParcelBagsListFromDb.Count; i++)
            {
                var currentParcelBag = shipmentParcelBagsListFromDb.ElementAtOrDefault(i);
                currentParcelBag.IsFinalized = true;
                await _ParcelBagService.Update(currentParcelBag);
            }
            for (int i = 0; i < shipmentLetterBagsListFromDb.Count; i++)
            {
                var currentLetterBag = shipmentLetterBagsListFromDb.ElementAtOrDefault(i);
                currentLetterBag.IsFinalized = true;
                await _LetterBagService.Update(currentLetterBag);               
            }

            var finalBag = new FinalBag();
            finalBag.CreatedTime = DateTime.UtcNow;
            finalBag.LetterBagList = shipmentLetterBagsListFromDb;
            finalBag.ParcelBagList = shipmentParcelBagsListFromDb;

            await _FinalBagService.Add(finalBag);

            shipment.BagList = finalBag;
            shipment.IsFinalized = true;

            await _ShipmentService.Update(shipment);
            return new ObjectResult("Done") { StatusCode = 200 };
        }

    }
}
