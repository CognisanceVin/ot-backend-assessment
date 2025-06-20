using OT.Assessment.Domain.Entities.Common;

namespace OT.Assessment.Domain.Entities
{
    public class Account : AuditTrackingBase
    {

        public double Balance { get; set; } = default!;
        public Player Player { get; set; }
        public Guid PlayerId { get; set; }
    }
}
