using Collections.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Collections.Web.Application.Extension
{
    public static class DatabaseExtension
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options
                => options.UseSqlServer(connectionString));

            services.BuildServiceProvider().GetService<ApplicationDbContext>()?.Database.Migrate();

            return services;
        }
    }
}