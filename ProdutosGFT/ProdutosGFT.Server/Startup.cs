using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProdutosGFT.Data;
using ProdutosGFT.Data.Repositories;
using ProdutosGFT.Data.Repositories.Generics;
using ProdutosGFT.Domain.Interfaces.Generics.Repositories;
using ProdutosGFT.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using ProdutosGFT.Server.v1.DTOs;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using ProdutosGFT.Server.Util;

namespace ProdutosGFT.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ProdutosGFTDbContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection"));
            });

            services
                .AddControllers(options =>
                {
                    options.Filters.Add(new ProducesResponseTypeAttribute(typeof(BadRequestDTO), 400));
                    options.Filters.Add(new ProducesResponseTypeAttribute(typeof(NotFoundDTO), 404));
                    options.Filters.Add(new ProducesResponseTypeAttribute(typeof(InternalErrorDTO), 500));
                })
                .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())); ;

            services.ConfigureJWT(hasAdminPolicy: true);
            services.ConfigureVersioning();
            services.ConfigureSwagger(title: "ProdutosGFT API", description: "API que simula um sistema de Venda", hasJWT: true, hasXmlComments: true);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Repositorios
            services.AddTransient(typeof(IDefaultRepository<>), typeof(DefaultRepository<>));

            services.AddScoped(typeof(IClienteRepository), typeof(ClienteRepository));
            services.AddScoped(typeof(IFornecedorRepository), typeof(FornecedorRepository));
            services.AddScoped(typeof(IProdutoRepository), typeof(ProdutoRepository));
            services.AddScoped(typeof(IVendaRepository), typeof(VendaRepository));

            //Services
            services.AddServicesV1();
            services.AddServicesV2();
        }

        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            #region Swagger

            app.UseSwagger(); //Gerar um arquivo JSON -> Swagger.json

            app.UseSwaggerUI(options =>
            {

                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                    url: $"/swagger/{description.GroupName}/swagger.json",
                    name: description.GroupName.ToUpperInvariant()
                    );
                }


                options.DocExpansion(DocExpansion.List);
            });

            #endregion
        }
    }
}
