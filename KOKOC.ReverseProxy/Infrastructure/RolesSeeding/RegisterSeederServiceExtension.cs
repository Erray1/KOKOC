using KOKOC.ReverseProxy.Infrastructure.Seeding;

namespace KOKOC.ReverseProxy.Infrastructure.RolesSeeding
{
    public static class RegisterSeederServiceExtension
    {
        public static IServiceCollection AddSeedingServices(this IServiceCollection services)
        {
            services.AddHostedService<SeedingService>();
            return services;
        }
    }
}
