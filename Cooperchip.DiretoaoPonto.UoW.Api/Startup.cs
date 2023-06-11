using Cooperchio.DiretoAoPonto.Uow.Data.Orm;
using Cooperchip.DiretoaoPonto.UoW.Api.Configuration.Settings;
using Cooperchip.DiretoaoPonto.UoW.Api.Mapper;
using Cooperchip.DiretoAoPonto.Uow.Data.FailedRepository;
using Cooperchip.DiretoAoPonto.Uow.Data.Repositories.Abstraction;
using Cooperchip.DiretoAoPonto.Uow.Data.Repositories.Implemetation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace Cooperchip.DiretoaoPonto.UoW.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get;}

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperConfig));

            services.AddScoped<IPassengerFailedRepository, PassengerFailedRepository>();
            services.AddScoped<IFlightFailedRepository, FlightFailedRepository>();

            services.AddScoped<IPassagenrRepository, PassengerRepository>();
            services.AddScoped<IFlightRepository, FlightRepository>();

            var connection = Configuration["MySQlConnection:MySQlConnectionString"];

            services.AddDbContext<UoWDbContext>(options => options.
                UseMySql(connection,
                        new MySqlServerVersion(
                            new Version(8, 0, 5))));

            services.AddControllers();

                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cooperchip.DiretoaoPonto.UoW.Api - API", Version = "v1" });
                });

            services.Configure<FlightSettings>(Configuration.GetSection(FlightSettings.SessionName));
            services.AddSingleton(s => s.GetRequiredService<IOptions<FlightSettings>>().Value);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) 
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI( c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Unit Of Work, Uow - API");
                });
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
