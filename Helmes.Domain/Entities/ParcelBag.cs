using Helmes.Domain.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Helmes.Domain.Entities
{
    public partial class ParcelBag : EntityBase<int>
    {
        [Display(Name = "Bag number")]
        [RegularExpression(@"^[0-9a-zA-Z''-'\s]{1,15}$",ErrorMessage = "Special characters are not  allowed.")]
        [StringLength(15, ErrorMessage = "The {0} value cannot exceed {1} characters.  ")]
        public string BagNumber { get; set; }

        [Display(Name = "List of parcels")]
        public ICollection<Parcel> ParcelList { get; set; }

        public int ShipmentId { get; set; }

        [DefaultValue(false)]
        public bool IsFinalized { get; set; }

    }
}
