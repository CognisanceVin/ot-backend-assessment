using OT.Assessment.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace OT.Assessment.Domain.Entities
{
    public class GameTransaction : AuditTrackingBase
    {
        [Key]
        public Guid CasinoWagerId { get; set; }
        public Wager CasinoWager { get; set; } = default!;
        public string WagerId { get; set; } = string.Empty;
        public string Theme { get; set; } = string.Empty;
        public string Provider { get; set; } = string.Empty;
        public string GameName { get; set; } = string.Empty;
        public string TransactionId { get; set; } = string.Empty;
        public string BrandId { get; set; } = string.Empty;
        public string AccountId { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string ExternalReferenceId { get; set; } = string.Empty;
        public string TransactionTypeId { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int NumberOfBets { get; set; }
        public string CountryCode { get; set; } = string.Empty;
        public string SessionData { get; set; } = string.Empty;
        public int Duration { get; set; }

        public Account? Account { get; set; }
    }
}
