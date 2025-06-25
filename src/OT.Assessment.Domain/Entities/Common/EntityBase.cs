using System.ComponentModel.DataAnnotations;

namespace OT.Assessment.Domain.Entities.Common
{
    public abstract class EntityBase
    {
        [Key]
        public Guid Id { get; set; }
    }
}
