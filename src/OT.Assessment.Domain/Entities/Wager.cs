using OT.Assessment.Domain.Entities.Common;

namespace OT.Assessment.Domain.Entities
{
    public class Wager : AuditTrackingBase
    {
        public Guid AccountId { get; set; }
        public Guid GameId { get; set; }
        public double Amount { get; set; }
        public DateTime CreatedAt { get; set; }

        public Account Account { get; set; }
        public Game Game { get; set; }
    }
}
