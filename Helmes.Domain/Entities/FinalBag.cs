using Helmes.Domain.Base;
using System.Collections.Generic;

namespace Helmes.Domain.Entities
{
    public partial class FinalBag : EntityBase<int>
    {
        public ICollection<ParcelBag> ParcelBagList { get; set; }
        public ICollection<LetterBag> LetterBagList { get; set; }
    }
}
