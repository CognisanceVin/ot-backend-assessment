using OT.Assessment.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace OT.Assessment.Domain.Entities.AuditTrail
{
    public class Transaction : AuditTrackingBase
    {
        [Key]
        public Guid EntityId { get; set; }
        public string EntityType { get; set; } = default!;
        public string Action { get; set; } = default!;
        public string Metadata { get; set; } = default!;
    }
}
