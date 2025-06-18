using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OT.Assessment.Application.Interfaces.Services;
using OT.Assessment.Application.Services;
using OT.Assessment.Domain.Interfaces.Repositories;
using OT.Assessment.Infrastructure.Messaging.RabbitMq;
using OT.Assessment.Infrastructure.Messaging.RabbitMq.Configs;
using OT.Assessment.Infrastructure.Persistance;
using OT.Assessment.Infrastructure.Persistance.Repositories;

namespace OT.Assessment.Infrastructure
{
    public static class LibraryRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OTAssessmentDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DatabaseConnection")));

            var rabbitMqConfig = configuration.GetSection("RabbitMq");
            services.Configure<RabbitMQOptions>(rabbitMqConfig);

            services.AddSingleton<RabbitMqPublisher>();
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IGameRepository, GameRepository>();

            return services;
        }
    }
}
