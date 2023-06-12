using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Cooperchip.DiretoaoPonto.UoW.Api.Configuration.Extensions
{
    public static class SwaggerConfigExtension
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<SwaggerDefaultValues>();

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Insira o token JWT desta maneira: Bearer {seu token}",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new string[] {}
                    }
                });
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
            });

            return app;
        }

        public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
        {
            readonly IApiVersionDescriptionProvider provider;
            public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => this.provider = provider;
            public void Configure(SwaggerGenOptions options)
            {
                foreach (var description in provider.ApiVersionDescriptions)
                    options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
            static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
            {
                var info = new OpenApiInfo()
                {
                    Title = "Cooperchip.DiretoaoPonto.UoW.Api - API",
                    Version = description.ApiVersion.ToString(),
                    Description = "Esta API faz parte do projeto Direto ao Ponto, desenvolvido por Cooperchip.",
                    Contact = new OpenApiContact() { Name = "Anderson Costa", Email = "andersonmtb88@gmail.com" }
                };

                if (description.IsDeprecated) info.Description += "*** Esta veresão está obsoleta! ***";
                
                return info;
            }
        }

        public class SwaggerDefaultValues : IOperationFilter
        {
            public void Apply(OpenApiOperation operation, OperationFilterContext context)
            {
                if (operation.Parameters == null) return;

                foreach (var parameter in operation.Parameters)
                {
                    var apiDescription = context.ApiDescription
                        .ParameterDescriptions
                        .First(p => p.Name == parameter.Name);

                    var routeInfo = apiDescription.RouteInfo;

                    operation.Deprecated = OpenApiOperation.DeprecatedDefault;

                    if (parameter.Description == null) parameter.Description = apiDescription.ModelMetadata?.Description;

                    if(routeInfo == null) continue;

                    if (parameter.In != ParameterLocation.Path && parameter.Schema.Default == null) parameter.Schema.Default = new OpenApiString(apiDescription.DefaultValue!.ToString());

                    parameter.Required |= !routeInfo.IsOptional;
                }
            }
        }
    }
}
