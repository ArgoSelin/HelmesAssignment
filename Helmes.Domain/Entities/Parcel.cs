using Helmes.Domain.Base;
using Helmes.Helpers.CustomValidation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Helmes.Domain.Entities
{
    public partial class Parcel : EntityBase<int>
    {

        [Display(Name = "Parcel number")]
        [RegularExpression(@"^[a-zA-Z]{2}[1-9]{6}[a-zA-Z]{2}$", ErrorMessage = "Incorrect Parcel Number")]
        public string ParcelNumber { get; set; }

        [Display(Name = "Recipient name")]
        [StringLength(100, ErrorMessage = "The {0} value cannot exceed {1} characters.  ")]
        public string RecipientName { get; set; }

        [Display(Name = "Destination country")]
        [CountryCodeValidator]
        public string DestinationCountry { get; set; }

        [RegularExpression(@"^\d+(.\d{1,3})?$")]
        public decimal Weight { get; set; }

        [RegularExpression(@"^\d+(.\d{1,2})?$")]
        public decimal Price { get; set; }

        public int ParcelBagId { get; set; }

    }
}
