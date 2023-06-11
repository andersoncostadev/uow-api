using Microsoft.OpenApi.Models;

namespace Cooperchip.DiretoaoPonto.UoW.Api.Configuration.Extensions
{
    public static class SwaggerConfigExtension
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cooperchip.DiretoaoPonto.UoW.Api - API", Version = "v1" });
            });

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
