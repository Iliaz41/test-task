using Microsoft.EntityFrameworkCore;
using Fitness.Infrastructure.Context;

namespace Fitness.API.Extensions
{
    public static class AddPostgreSQLExtension
    {
        public static IServiceCollection AddPostgresDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(x =>
                x.UseNpgsql(configuration.GetConnectionString("PostgreConnection")));

            return services;
        }
    }
}
