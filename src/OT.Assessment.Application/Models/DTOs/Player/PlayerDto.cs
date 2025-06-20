using OT.Assessment.Application.Models.DTOs.Account;

namespace OT.Assessment.Application.Models.DTOs.Player
{
    public class PlayerDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string EmailAddress { get; set; }
        public string CountryCode { get; set; }
        public AccountDto Account { get; set; }
    }
}
