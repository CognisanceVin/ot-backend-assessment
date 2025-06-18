using OT.Assessment.Domain.Entities.Common;

namespace OT.Assessment.Domain.Entities
{
    public class Wager : AuditTrackingBase
    {
        public string WagerId { get; set; } = string.Empty;

        public string Theme { get; set; } = string.Empty;
        public string Provider { get; set; } = string.Empty;
        public string GameName { get; set; } = string.Empty;
        public string TransactionId { get; set; } = string.Empty;

        public string BrandId { get; set; } = string.Empty;

        public Guid AccountId { get; set; } // FK to Account entity
        public Account? Account { get; set; }

        public string Username { get; set; } = string.Empty;
        public string ExternalReferenceId { get; set; } = string.Empty;
        public string TransactionTypeId { get; set; } = string.Empty;

        public decimal Amount { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int NumberOfBets { get; set; }

        public string CountryCode { get; set; } = string.Empty;
        public string SessionData { get; set; } = string.Empty;
        public int Duration { get; set; }
    }
}
