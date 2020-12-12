using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProdutosGFT.Domain.Config;
using ProdutosGFT.Domain.Util.Enums;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ProdutosGFT.Server.Util
{
    public static class StartupExtensions
    {

        public static IServiceCollection AddServicesV1(this IServiceCollection services)
        {
            services.AddScoped(typeof(v1.Interfaces.Services.IClienteService), typeof(v1.Services.ClienteService));
            services.AddScoped(typeof(v1.Interfaces.Services.IFornecedorService), typeof(v1.Services.FornecedorService));
            services.AddScoped(typeof(v1.Interfaces.Services.IProdutoService), typeof(v1.Services.ProdutoService));
            services.AddScoped(typeof(v1.Interfaces.Services.IVendaService), typeof(v1.Services.VendaService));

            services.AddTransient(typeof(v1.Interfaces.Services.IHateoasService<>), typeof(v1.Services.Hateoas.HateoasService<>));

            return services;
        }

        public static IServiceCollection AddServicesV2(this IServiceCollection services)
        {
            services.AddScoped(typeof(v2.Interfaces.Services.IVendaService), typeof(v2.Services.VendaService));

            return services;
        }

        public static void ConfigureVersioning(this IServiceCollection services)
        {

            services.AddApiVersioning(p =>
                      {
                          p.DefaultApiVersion = new ApiVersion(1, 0);
                          p.ReportApiVersions = true;
                          p.AssumeDefaultVersionWhenUnspecified = true;
                      });

            services.AddVersionedApiExplorer(p =>
                      {
                          p.GroupNameFormat = "'v'VVV";
                          p.SubstituteApiVersionInUrl = true;
                      });
        }

        public static void ConfigureJWT(this IServiceCollection services, bool hasAdminPolicy = false) // Adicionar app.UseAuthentication() -> Antes do UseAuthorization()
        {
            var key = Encoding.ASCII.GetBytes(Settings.JWTKey);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            if (hasAdminPolicy)
            {
                services.AddAuthorization(options =>
                {
                    options.AddPolicy("Admin", policy => policy.RequireRole(new string[] { Role.ADMIN.ToString() }));
                });
            }
        }

        public static void ConfigureSwagger(this IServiceCollection services, string description, string title, bool hasVersioning = false, bool hasJWT = false, bool hasXmlComments = false)
        {

            services.AddSwaggerGen(options =>
            {
                var contact = new OpenApiContact()
                {
                    Email = "lucas.cancio@gft.com",
                    Name = "Lucas Camargo Cancio",
                    Url = new Uri("https://www.linkedin.com/in/lucas-camargo-cancio/")
                };

                var license = new OpenApiLicense()
                {
                    Name = "MIT License",
                    Url = new Uri("https://opensource.org/licenses/MIT")
                };

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = $"{title} Versão 1.0",
                    Description = description,
                    Contact = contact,
                    License = license
                });

                options.SwaggerDoc("v2", new OpenApiInfo
                {
                    Version = "v2",
                    Title = $"{title} Versão 2.0",
                    Description = description,
                    Contact = contact,
                    License = license
                });

                options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                if (hasJWT) options.ConfigureJWTInSwagger();


                if (hasXmlComments) options.ConfigureXmlCommentsInSwagger();

            });
        }

        private static void ConfigureXmlCommentsInSwagger(this SwaggerGenOptions options)
        {
            var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

            string[] xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.AllDirectories);

            foreach (string xmlFile in xmlFiles)
            {
                options.IncludeXmlComments(xmlFile);
            }
        }

        private static void ConfigureJWTInSwagger(this SwaggerGenOptions options)
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Insira o token JWT aqui. Exemplo: \"Authorization: Bearer {token}\" "
            });



            options.AddSecurityRequirement(
                    new OpenApiSecurityRequirement{
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                        }
                    }
            );
        }
    }
}