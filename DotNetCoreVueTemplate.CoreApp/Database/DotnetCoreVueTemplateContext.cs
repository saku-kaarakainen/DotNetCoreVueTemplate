using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetCoreVueTemplate.CoreApp.Database
{
    public interface IDbContext 
    {
        void Populate();
    }

    /// <summary>
    /// Code First Context.
    /// </summary>
    public class DotnetCoreVueTemplateContext : DbContext, IDbContext
    {
        public DotnetCoreVueTemplateContext(DbContextOptions<DotnetCoreVueTemplateContext> options)
            : base(options)
        {

        }

        public DbSet<WeatherForecast> WeatherForecasts { get; set; }

        // Note that the data might not be in order if you are going to use asynchronous context.
        public void Populate()
        {
            var context = this;
            context.Database.EnsureCreated(); // this might be redundant line

            if (context.WeatherForecasts.Any())
            {
                return; // DB has been seeded
            }

            var summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            var rng = new Random();
            var data = Enumerable.Range(1, 20).Select(index => new Database.WeatherForecast
            {
                Date = DateTime.Now.AddDays(index - 10),
                TemperatureC = rng.Next(-20, 55),
                Summary = summaries[rng.Next(summaries.Length)]
            });

            // This is the "database"
            context.WeatherForecasts.AddRange(data);

            context.SaveChanges();
        }
    }

    public class WeatherForecast
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [StringLength(100)]
        public string Summary { get; set; }

        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }
    }
}
