using DotNetCoreVueTemplate.CoreApp.Components;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VueCliMiddleware;

namespace DotNetCoreVueTemplate.CoreApp
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
            // NOTE: PRODUCTION Ensure this is the same path that is specified in your webpack output
            services.AddSpaStaticFiles(opt => opt.RootPath = "../ClientApp/dist");

            services.AddControllersWithViews();
            services.AddSingleton(Configuration);
            services.AddScoped<HandleExceptionFilter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // PRODUCTION uses webpack static files
            app.UseSpaStaticFiles();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapToVueCliProxy(
                    "{*path}",
                    new SpaOptions { SourcePath = $"../{Extensions.GetNamespaceRoot<Startup>()}.ClientApp" },
                    npmScript: Debugger.IsAttached ? "serve" : null,
                    regex: "Compiled successfully",
                    forceKill: true,
                    wsl: false // Set to true if you are using WSL on windows. For other operating systems it will be ignored
                );
            });
        }
    }

    public static class Extensions
    {
        public static string GetNamespaceRoot<TType>() => typeof(TType).Namespace.Split('.')[0];
    }
}
