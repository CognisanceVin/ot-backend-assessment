namespace OT.Assessment.Application.Models.Request
{
    public class CreatePlayerRequestModel
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string CountryCode { get; set; }
    }
}
