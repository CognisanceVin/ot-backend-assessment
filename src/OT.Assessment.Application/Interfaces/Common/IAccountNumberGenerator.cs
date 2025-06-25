namespace OT.Assessment.Application.Interfaces.Common
{
    public interface IAccountNumberGenerator
    {
        Task<string> GenerateAccountNumber();
    }
}
