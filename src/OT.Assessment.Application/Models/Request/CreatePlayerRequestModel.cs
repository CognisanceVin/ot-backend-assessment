namespace OT.Assessment.Application.Models.Request
{
    public class CreatePlayerRequestModel
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //public Account? Account { get; set; }
        public Guid AccountId { get; set; }
        public string UserId { get; set; }
        public decimal Amount { get; set; }
        public string TransactionTypeId { get; set; }
        public int NumberOfBets { get; set; }
        public string CountryCode { get; set; }
        public int Duration { get; set; }
    }
}
