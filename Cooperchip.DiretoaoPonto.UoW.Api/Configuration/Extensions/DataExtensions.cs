using Cooperchio.DiretoAoPonto.Uow.Data.Orm;
using Cooperchip.DiretoaoPonto.UoW.Api.Configuration.Settings;
using Cooperchip.DiretoAoPonto.Uow.Data.FailedRepository;
using Cooperchip.DiretoAoPonto.Uow.Data.Repositories.Abstraction;
using Cooperchip.DiretoAoPonto.Uow.Data.Repositories.Implemetation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace Cooperchip.DiretoaoPonto.UoW.Api.Configuration.Extensions
{
    public static class DataExtensions
    {
        public static IServiceCollection AddDbContextConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration["MySQlConnection:MySQlConnectionString"];
            services.AddDbContext<UoWDbContext>(options => options.
                           UseMySql(connection,new MySqlServerVersion(new Version(8, 0, 5))));
            services.AddControllers();

            return services;
        }

        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cooperchip.DiretoaoPonto.UoW.Api - API", Version = "v1" });
            });
       
            return services;
        }

        public static IServiceCollection AddDIRepositoryConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IPassengerFailedRepository, PassengerFailedRepository>();
            services.AddScoped<IFlightFailedRepository, FlightFailedRepository>();

            services.AddScoped<IPassagenrRepository, PassengerRepository>();
            services.AddScoped<IFlightRepository, FlightRepository>();

            return services;
        }

        public static IServiceCollection AddAppSettingsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<FlightSettings>(configuration.GetSection(FlightSettings.SessionName));
            services.AddSingleton(s => s.GetRequiredService<IOptions<FlightSettings>>().Value);

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Unit Of Work, Uow - API");
            });

            return app;
        }
    }
}
