using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OT.Assessment.Application.Mappings;

namespace OT.Assessment.Application
{
    public static class LibraryRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(GameProfile).Assembly);
            services.AddAutoMapper(typeof(PlayerProfile).Assembly);
            services.AddAutoMapper(typeof(WagerProfile).Assembly);

            return services;
        }
    }
}
