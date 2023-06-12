using Cooperchip.DiretoaoPonto.UoW.Api.Configuration.Extensions;
using Cooperchip.DiretoaoPonto.UoW.Api.Mapper;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using static Cooperchip.DiretoaoPonto.UoW.Api.Configuration.Extensions.SwaggerConfigExtension;

namespace Cooperchip.DiretoaoPonto.UoW.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiConfig();

            services.AddAutoMapper(typeof(AutoMapperConfig));

            services.AddDIRepositoryConfiguration();
            services.AddDbContextConfiguration(Configuration);
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerConfiguration();
            services.AddAppSettingsConfiguration(Configuration);

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerConfiguration(provider);
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
