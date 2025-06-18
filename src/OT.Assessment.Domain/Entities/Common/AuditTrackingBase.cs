namespace OT.Assessment.Domain.Entities.Common
{
    public abstract class AuditTrackingBase : EntityBase
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
