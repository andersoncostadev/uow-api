using Cooperchio.DiretoAoPonto.Uow.Data.Orm;
using Microsoft.EntityFrameworkCore;

namespace Cooperchip.DiretoaoPonto.UoW.Api.Configuration.Extensions
{
    public static class DataExtensions
    {
        public static IServiceCollection AddDbContextConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration["MySQlConnection:MySQlConnectionString"];
            services.AddDbContext<UoWDbContext>(options => options.
                           UseMySql(connection, new MySqlServerVersion(new Version(8, 0, 5))));
            services.AddControllers();

            return services;
        }
    }
}
