namespace OT.Assessment.Application.Services
{
    public class WagerService : IWagerService
    {
        private readonly IRabbitMqPublisher _publisher;
        private readonly IMapper _mapper;

        public WagerService(IRabbitMqPublisher publisher, IMapper mapper)
        {
            _publisher = publisher;
            _mapper = mapper;
        }

        public async Task<Result> CreateWager(WagerDto dto)
        {
            var message = _mapper.Map<WagerMessage>(dto);
            await _publisher.PublishAsync(message, "wager.queue");
            return Result.Success();
        }
    }
}
}
