using Helmes.Domain.Entities;
using Helmes.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Helmes.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class BagController : ControllerBase
    {
        private readonly IShipmentService _ShipmentService;
        private readonly IParcelBagService _ParcelBagService;
        private readonly ILetterBagService _LetterBagService;
        private readonly IParcelService _ParcelService;

        public BagController(IShipmentService ShipmentService, 
            IParcelBagService ParcelBagService, 
            ILetterBagService LetterBagService, 
            IParcelService ParcelService)
        {
            _ShipmentService = ShipmentService;
            _ParcelBagService = ParcelBagService;
            _LetterBagService = LetterBagService;
            _ParcelService = ParcelService;
        }

        [HttpPost("CreateParcelBag")]
        public async Task<IActionResult> CreateParcelBag(ParcelBag ParcelBag)
        {
            var shipment = await _ShipmentService.GetOne(ParcelBag.ShipmentId);
            ParcelBag.CreatedTime = DateTime.UtcNow;
            ParcelBag.IsFinalized = false;
            if (shipment.IsFinalized == true)
            {
                return new ObjectResult("Cannot add bags to finalized shipment") { StatusCode = 400 };
            }
            else
            {
                await _ParcelBagService.Add(ParcelBag);
                return new ObjectResult("Parcelbag created") { StatusCode = 200 };
            }            
        }

        [HttpPost("CreateLetterBag")]
        public async Task<IActionResult> CreateLetterBag(LetterBag LetterBag)
        {
            var shipment = await _ShipmentService.GetOne(LetterBag.ShipmentId);
            LetterBag.CreatedTime = DateTime.UtcNow;
            LetterBag.IsFinalized = false;

            if (shipment.IsFinalized == true)
            {
                return new ObjectResult("Cannot add bags to finalized shipment") { StatusCode = 400 };
            }
            else
            {
                await _LetterBagService.Add(LetterBag); 
                return new ObjectResult("Letterbag created") { StatusCode = 200 };
            }

        }

        [HttpGet("GetShipmentLetterBagList")]
        public async Task<ActionResult<List<LetterBag>>> GetShipmentLetterBagList(int id)
        {
            var shipmentLetterBagsListFromDb = await _LetterBagService.GetShipmentLetterBagsByShipmentIdService(id);
            return shipmentLetterBagsListFromDb;

        }

        [HttpGet("GetShipmentParcelBagList")]
        public async Task<ActionResult<List<ParcelBag>>> GetShipmentParcelBagList(int id)
        {
            var shipmentParcelBagsListFromDb = await _ParcelBagService.GetShipmentParcelBagsByShipmentIdService(id);
            return shipmentParcelBagsListFromDb;

        }

    }
}
