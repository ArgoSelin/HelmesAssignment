using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Helmes.Domain.Base
{
    public interface IEntityBase<TKey>
    {
        int Id { get; set; }
    }

    public abstract class EntityBase<TKey> : IEntityBase<TKey>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        public virtual DateTime CreatedTime { get; set; }
    }
}
