using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using REAM.ApiEnvio.Config;
using REAM.ApiEnvio.Helper;

namespace REAM.ApiEnvio
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
            //string bus = Configuration.GetValue<string>("BusUrl");
            services.AddOptions();
            services.Configure<ParametroConfig>(Configuration);

            services.AddSingleton<EnvioTopicHelper>();

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
        }
    }
}
