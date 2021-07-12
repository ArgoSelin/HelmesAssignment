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
    public class ParcelController : ControllerBase
    {
        private readonly IParcelBagService _ParcelBagService;
        private readonly IParcelService _ParcelService;

        public ParcelController(
            IParcelBagService ParcelBagService, 
            IParcelService ParcelService)
        {
            _ParcelBagService = ParcelBagService;
            _ParcelService = ParcelService;
        }

        [HttpPost("CreateParcel")]
        public async Task<IActionResult> CreateParcel(Parcel Parcel)
        {
            var parcelBag = await _ParcelBagService.GetOne(Parcel.ParcelBagId);
            Parcel.CreatedTime = DateTime.UtcNow;
            Parcel.ParcelBagId = Parcel.ParcelBagId;

            if (parcelBag.IsFinalized == true)
            {
                return new ObjectResult("Cannot add parcels to finalized bag") { StatusCode = 400 };
            }
            else
            {
             await _ParcelService.Add(Parcel);
                
             return new ObjectResult("Parcel created") { StatusCode = 200 };
            }
        }

        [HttpPost("CreateParcels")]
        public async Task<IActionResult> CreateParcels(string BagNumber, List<Parcel> ParcelList)
        {
            var parcelBag = await _ParcelBagService.GetParcelBagByBagNumberService(BagNumber);
            var parcelBagId = parcelBag.Id;

            if(parcelBag.IsFinalized == true)
            {
                return new ObjectResult("Cannot add parcels to finalized bag") { StatusCode = 400 };
            }
            else
            {
                for (int i = 0; i < ParcelList.Count; i++)
                {
                    var currentParcel = ParcelList.ElementAtOrDefault(i);
                    currentParcel.ParcelBagId = parcelBagId;

                    await _ParcelService.Add(currentParcel);
                }
                return new ObjectResult("Parcels created") { StatusCode = 200 };
            }
        }

    }
}
