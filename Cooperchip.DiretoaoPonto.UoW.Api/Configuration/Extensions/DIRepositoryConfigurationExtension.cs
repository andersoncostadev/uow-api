using Cooperchip.DiretoAoPonto.Uow.Data.FailedRepository;
using Cooperchip.DiretoAoPonto.Uow.Data.Repositories.Abstraction;
using Cooperchip.DiretoAoPonto.Uow.Data.Repositories.Implemetation;

namespace Cooperchip.DiretoaoPonto.UoW.Api.Configuration.Extensions
{
    public class DIRepositoryConfigurationExtension
    {
        public static IServiceCollection AddDIRepositoryConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IPassengerFailedRepository, PassengerFailedRepository>();
            services.AddScoped<IFlightFailedRepository, FlightFailedRepository>();

            services.AddScoped<IPassagenrRepository, PassengerRepository>();
            services.AddScoped<IFlightRepository, FlightRepository>();

            return services;
        }
    }
}
