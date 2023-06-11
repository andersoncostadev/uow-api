using Cooperchip.DiretoAoPonto.Uow.Data.FailedRepository;
using Cooperchip.DiretoAoPonto.Uow.Data.Repositories.Abstraction;
using Cooperchip.DiretoAoPonto.Uow.Data.Repositories.Implemetation;
using Cooperchip.DiretoAoPonto.Uow.Data.Repositories.V2.Abstraction;
using Cooperchip.DiretoAoPonto.Uow.Data.Repositories.V2.Implemetation;

namespace Cooperchip.DiretoaoPonto.UoW.Api.Configuration.Extensions
{
    public static class DIRepositoryConfigurationExtension
    {
        public static IServiceCollection AddDIRepositoryConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IPassengerFailedRepository, PassengerFailedRepository>();
            services.AddScoped<IFlightFailedRepository, FlightFailedRepository>();

            services.AddScoped<IPassagenrRepository, PassengerRepository>();
            services.AddScoped<IFlightRepository, FlightRepository>();

            //V2 Approach
            services.AddScoped<IPassagenrV2Repository, PassagenrV2Repository>();
            services.AddScoped<IFlightV2Repository, FlightV2Repository>();
            services.AddScoped<IUnitOfWorkV2, UnitOfWork>();

            return services;
        }
    }
}
