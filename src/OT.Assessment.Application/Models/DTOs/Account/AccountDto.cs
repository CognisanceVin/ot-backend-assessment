namespace OT.Assessment.Application.Models.DTOs.Account
{
    public class AccountDto
    {
        public Guid Id { get; set; }
        public string AccountNumber { get; set; }
        public Guid PlayerId { get; set; }
        public double Balance { get; set; } = default!;
    }
}
