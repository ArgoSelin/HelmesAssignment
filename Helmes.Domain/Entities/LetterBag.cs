using Helmes.Domain.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Helmes.Domain.Entities
{
    public partial class LetterBag : EntityBase<int>
    {
        [Display(Name = "Bag Number")]
        [RegularExpression(@"^[0-9a-zA-Z''-'\s]{1,15}$",ErrorMessage = "special characters are not  allowed.")]
        [StringLength(15, ErrorMessage = "The {0} value cannot exceed {1} characters.  ")]
        public string BagNumber { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int LetterCount { get; set; }

        [RegularExpression(@"^\d+(.\d{1,3})?$")]
        public decimal Weight { get; set; }

        [RegularExpression(@"^\d+(.\d{1,2})?$")]
        public decimal Price { get; set; }

        public int ShipmentId { get; set; }

        [DefaultValue(false)]
        public bool IsFinalized { get; set; }

    }
}
