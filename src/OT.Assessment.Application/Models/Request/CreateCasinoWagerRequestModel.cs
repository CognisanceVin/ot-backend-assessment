namespace OT.Assessment.Application.Models.Requests
{
    public class CreateCasinoWagerRequestModel
    {
        public string GameName { get; set; }
        public string TransactionId { get; set; }
        public Guid AccountId { get; set; }
        public string UserId { get; set; }
        public decimal Amount { get; set; }
        public string TransactionTypeId { get; set; }
        public int NumberOfBets { get; set; }
        public string CountryCode { get; set; }
        public int Duration { get; set; }
    }
}
