namespace OT.Assessment.Application.Models.DTOs.Wager
{
    public class WagerDto
    {
        public Guid AccountId { get; set; }
        public Guid GameId { get; set; }
        public double Amount { get; set; }
    }
    public class WagerMessage
    {
        public Guid AccountId { get; set; }
        public Guid GameId { get; set; }
        public double Amount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
