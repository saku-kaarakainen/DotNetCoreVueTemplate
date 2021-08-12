using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreVueTemplate.CoreApp.Database
{
    /// <summary>
    /// Code First Context.
    /// </summary>
    public class DotnetCoreVueTemplateContext : DbContext
    {
        public DotnetCoreVueTemplateContext(DbContextOptions<DotnetCoreVueTemplateContext> options)
            : base(options)
        {

        }

        public DbSet<Summary> Summaries { get; set; }

        // Note that the data might not be in order if you are using asynchronous context.
        public void PopulateDatabase()
        {
            var context = this;
            
            context.Summaries.AddRange(new List<string>
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            }.Select(str => new Summary() { Name = str }));

            context.SaveChanges();
        }
    }

    public class Summary
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }
    }
}
