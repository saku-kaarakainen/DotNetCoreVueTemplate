using DotNetCoreVueTemplate.CoreApp.Components;
using DotNetCoreVueTemplate.CoreApp.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
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
            services.AddAutoMapper(typeof(Startup));

            // NOTE: PRODUCTION Ensure this is the same path that is specified in your webpack output
            services.AddSpaStaticFiles(opt => opt.RootPath = "../ClientApp/dist");

            services.AddControllersWithViews();

            // As you can see, I am using a real database. 
            // Check appsettings.json, This one doesn't have any security.
            services.AddDbContext<Database.DotnetCoreVueTemplateContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
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

            app.MigrateDatabase<DotnetCoreVueTemplateContext>();

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

        /// <summary>
        /// Migrate any database changes on startup (includes initial database creation)
        /// </summary>
        public static IApplicationBuilder MigrateDatabase<TDatabase>(this IApplicationBuilder app)
            where TDatabase : DbContext, IDbContext
        {            
            using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<TDatabase>();
            
            // Creates the database if it doesn't exists
            context.Database.EnsureCreated();

            var serviceProvider = (context as IInfrastructure<IServiceProvider>).Instance;

            var differ = serviceProvider.GetRequiredService<IMigrationsModelDiffer>();
            var migrationsAssembly = serviceProvider.GetRequiredService<IMigrationsAssembly>();

            if (migrationsAssembly.ModelSnapshot != null)
            {
                // Migrations can only be done if there is any.
                IModel snapshot = migrationsAssembly.ModelSnapshot.Model;

                var dependencies = context.GetService<ProviderConventionSetBuilderDependencies>();
                var relationalDependencies = context.GetService<RelationalConventionSetBuilderDependencies>();

                TypeMappingConvention typeMappingConvention = new(dependencies);
                typeMappingConvention.ProcessModelFinalizing(((IConventionModel)snapshot).Builder, null);

                RelationalModelConvention relationalModelConvention = new(dependencies, relationalDependencies);
                var sourceModel = relationalModelConvention.ProcessModelFinalized(snapshot);
                var relationModel = ((IMutableModel)sourceModel).FinalizeModel().GetRelationalModel();

                if (differ.HasDifferences(relationModel, context.Model.GetRelationalModel()))
                {
                    throw new ApplicationException("There are differences between models of the database and this application. Have you run all migration scripts?");
                }
            }

            context.Database.Migrate();
            context.Populate();

            return app;
        }
    }
}
