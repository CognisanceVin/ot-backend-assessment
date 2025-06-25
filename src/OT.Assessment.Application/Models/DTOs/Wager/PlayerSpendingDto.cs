namespace OT.Assessment.Application.Models.DTOs.Wager
{
    public class PlayerSpendingDto
    {
        public Guid AccountId { get; set; }
        public string Username { get; set; }
        public double TotalSpent { get; set; }
    }
}
