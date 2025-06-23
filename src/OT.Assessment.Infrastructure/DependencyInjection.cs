using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OT.Assessment.Application;
using OT.Assessment.Application.Interfaces.Common.Messaging;
using OT.Assessment.Application.Interfaces.Services;
using OT.Assessment.Application.Services;
using OT.Assessment.Domain.Interfaces.Repositories;
using OT.Assessment.Infrastructure.Messaging.RabbitMq.Configs;
using OT.Assessment.Infrastructure.Messaging.RabbitMq.Publisher;
using OT.Assessment.Infrastructure.Persistance;
using OT.Assessment.Infrastructure.Persistence;
using OT.Assessment.Infrastructure.Repositories;
using RabbitMQ.Client;

namespace OT.Assessment.Infrastructure
{
    public static class DependencyInjection
    {
        public static async Task<IServiceCollection> AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OTAssessmentDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DatabaseConnection")));

            services.Configure<RabbitMQOptions>(configuration.GetSection("RabbitMq"));

            var options = configuration.GetSection("RabbitMq").Get<RabbitMQOptions>()!;

            var factory = new ConnectionFactory
            {
                HostName = options.Host,
                UserName = options.UserName,
                Password = options.Password,
                Port = options.Port,
                VirtualHost = options.VirtualHost
            };


            Console.WriteLine($"Publishing to RabbitMQ at {factory.HostName}:{factory.Port} vhost={factory.VirtualHost}");
            var connection = await factory.CreateConnectionAsync();

            services.AddSingleton<IConnection>(connection);

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IRabbitMqPublisher, RabbitMqPublisher>();
            services.AddSingleton<IWagerService, WagerService>();
            services.AddScoped<IWagerRepository, WagerRepository>();

            services.AddScoped<ITransactionRepository, TransactionRepository>();

            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();

            return services;
        }
    }
}
