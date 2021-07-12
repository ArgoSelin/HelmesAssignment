using Helmes.Domain.Base;
using Helmes.Helpers.CustomValidation;
using Helmes.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Helmes.Domain.Entities
{
    public partial class Shipment : EntityBase<int>
    {
        [Display(Name = "Shipment number")]
        [RegularExpression(@"^[a-zA-Z1-9]{3}-[a-zA-Z1-9]{6}$")]
        public string ShipmentNumber { get; set; }

        public AirportCodes Airport { get; set; }

        [Display(Name = "Flight number")]
        [RegularExpression(@"^[1-9]{2}[a-zA-Z]{4}$")]
        public string FlightNumber { get; set; }

        [Display(Name = "Flight date")]
        [DateMoreThanToToday]
        public DateTime FlightDate { get; set; }

        [Display(Name = "List of bags")]
        public FinalBag BagList { get; set; }

        [DefaultValue(false)]
        public bool IsFinalized { get; set; }
    }
}
