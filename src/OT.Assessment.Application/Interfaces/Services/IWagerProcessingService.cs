using OT.Assessment.Application.Models.DTOs.Wager;

namespace OT.Assessment.Application.Interfaces.Services
{
    public interface IWagerProcessingService
    {
        Task ProcessWager(WagerMessage message);
    }
}
