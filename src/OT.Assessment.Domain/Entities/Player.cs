using OT.Assessment.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace OT.Assessment.Domain.Entities
{
    public class Player : AuditTrackingBase
    {

        [Required]
        public string Username { get; set; } = default!;
        public string Firstname { get; set; } = default!;
        public string Lastname { get; set; } = default!;

        [Required]
        public string Email { get; set; } = default!;

        public ICollection<Account> Accounts { get; set; } = new List<Account>();
    }
}
