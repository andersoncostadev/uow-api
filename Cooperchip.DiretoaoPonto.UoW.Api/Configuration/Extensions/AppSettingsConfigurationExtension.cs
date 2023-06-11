using Cooperchip.DiretoaoPonto.UoW.Api.Configuration.Settings;
using Microsoft.Extensions.Options;

namespace Cooperchip.DiretoaoPonto.UoW.Api.Configuration.Extensions
{
    public static class AppSettingsConfigurationExtension
    {
        public static IServiceCollection AddAppSettingsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<FlightSettings>(configuration.GetSection(FlightSettings.SessionName));
            services.AddSingleton(s => s.GetRequiredService<IOptions<FlightSettings>>().Value);

            return services;
        }
    }
}
