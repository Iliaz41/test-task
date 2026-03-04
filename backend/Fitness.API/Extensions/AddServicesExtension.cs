using Fitness.Application.IServices;
using Fitness.Application.Services;

namespace Fitness.API.Extensions
{
    public static class AddServicesExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICalendarService, CalendarService>();

            return services;
        }
    }
}
