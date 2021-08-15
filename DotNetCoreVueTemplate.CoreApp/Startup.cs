using DotNetCoreVueTemplate.CoreApp.Components;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices;
using Microsoft.EntityFrameworkCore;
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

            // If you want to use Microsoft SQL database, uncomment this line below:
            // services.AddDbContext<Database.DotnetCoreVueTemplateContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DotnetCoreVueTemplateConnectionString")));
            //
            // After that you need to specify your connection string
            // I haven't done that yet so for now on, you need to figure it out by yourself.
            // Also, FYI this project has both Microsoft.EntityFrameworkCore.SqlServer and Microsoft.EntityFrameworkCore.InMemory, 
            // you may not need both.
            // For now, we are using in-memory database. So the data is saved in the memory of your IIS server
            services.AddDbContext<Database.DotnetCoreVueTemplateContext>(options => options.UseInMemoryDatabase("DotnetCoreVueTemplateContextDatabase"));
            // If you want to use in-memory database in your unit tests, I have been using this to create my mock database:
            /*
                var databaseName = Guid.NewGuid().ToString();
                var options = new DbContextOptionsBuilder<MerikarttapalauteContextReadWrite>()
                    .UseInMemoryDatabase(databaseName: databaseName)
                    .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning)) // NOTE: This removes db_transaction error messages -> allows tests to work!
                    .Options;
             */

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
