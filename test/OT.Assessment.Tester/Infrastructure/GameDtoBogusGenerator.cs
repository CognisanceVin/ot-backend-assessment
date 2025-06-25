using OT.Assessment.Application.Models.DTOs.Game;

namespace OT.Assessment.Tester.Infrastructure
{
    public class GameDtoBogusGenerator
    {
        private readonly Faker<GameDto> _faker;

        public GameDtoBogusGenerator()
        {
            Randomizer.Seed = new Random(2025);
            _faker = new Faker<GameDto>()
                .StrictMode(true)
                .RuleFor(g => g.Id, f => Guid.NewGuid())
                .RuleFor(g => g.Name, f => f.Commerce.ProductName())
                .RuleFor(g => g.GameCode, f => f.Random.AlphaNumeric(6).ToUpper());
        }

        public GameDto Generate() => _faker.Generate();
        public List<GameDto> Generate(int count) => _faker.Generate(count);
    }
}
