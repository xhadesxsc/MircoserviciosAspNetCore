using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RAEM.ApiSQL.Contexto;
using RAEM.ApiSQL.Helper;
using RAEM.ApiSQL.Services;

namespace RAEM.ApiSQL
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
            string cnnVentas = Configuration.GetValue<string>("CnnDbVentas");
            services.AddDbContext<DbVentasContext>(options => {
                options.UseSqlServer(cnnVentas);
            });
            services.AddOptions();
            services.Configure<ParametroConfig>(Configuration);
            services.AddTransient<ICompraServices, CompraServices>();
            services.AddSingleton<IReceiverTopicHelper, ReceiverTopicHelper>();
            services.AddSingleton<IProcesarData, ProcesarData>();

            string urlSeguridad = Configuration.GetValue<string>("UrlSeguridad");
            string nombreApi = Configuration.GetValue<string>("NombreApi");

            services.AddAuthentication("Bearer")
             .AddIdentityServerAuthentication(
                 options => {
                     options.Authority = urlSeguridad;
                     options.RequireHttpsMetadata = false;
                     options.ApiName = nombreApi;

                 }
             );


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseMvc();

            IReceiverTopicHelper suscriptor =
                app.ApplicationServices.GetService<IReceiverTopicHelper>();

            suscriptor.PreparaFiltrosMensaje().GetAwaiter().GetResult();

        }
    }
}
