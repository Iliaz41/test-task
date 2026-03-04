using Fitness.Application.Mapper;

namespace Fitness.API.Extensions
{
    public static class AddAutoMapperExtension
    {
        public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));

            return services;
        }
    }
}
