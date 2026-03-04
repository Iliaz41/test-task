using Fitness.Application.IRepositories;
using Fitness.Infrastructure.Repositories;

namespace Fitness.API.Extensions
{
    public static class AddRepositoriesExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICalendarRepository, CalendarRepository>();
            services.AddScoped<IExerciseRepository, ExerciseRepository>();

            return services;
        }
    }
}
