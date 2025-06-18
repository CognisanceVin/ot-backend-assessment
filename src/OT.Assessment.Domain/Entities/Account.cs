using OT.Assessment.Domain.Entities.Common;

namespace OT.Assessment.Domain.Entities
{
    public class Account : AuditTrackingBase
    {

        public string BrandId { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
        public ICollection<Wager> Wagers { get; set; } = new List<Wager>();
    }
}
