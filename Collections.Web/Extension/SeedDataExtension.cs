using Collections.Infrastructure.Data;

namespace Collections.Web.Extension
{
    public static class SeedDataExtension
    {
        public static IServiceCollection AddSeedData(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();

            ApplicationDbContextSeed.SeedEssentialsAsync(serviceProvider);

            return services;
        }
    }
}