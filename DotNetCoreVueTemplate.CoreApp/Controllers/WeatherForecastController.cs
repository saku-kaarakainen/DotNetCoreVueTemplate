using DotNetCoreVueTemplate.CoreApp.Components;
using DotNetCoreVueTemplate.CoreApp.Database;
using DotNetCoreVueTemplate.CoreApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreVueTemplate.CoreApp.Controllers
{
    // 2021-08-07: Class copied and modified from: https://github.com/SoftwareAteliers/asp-net-core-vue-starter/blob/master/Controllers/WeatherForecastController.cs
    [ApiController]
    [Route("api/[controller]")]
    [ServiceFilter(typeof(HandleExceptionFilter))] 
    public class WeatherForecastController : ControllerBase
    {
        private readonly DotnetCoreVueTemplateContext context;
        private readonly ILogger<WeatherForecastController> logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, DotnetCoreVueTemplateContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var rng = new Random();
            var summaries = await this.context.Summaries.Select(s => s.Name).ToArrayAsync();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = summaries[rng.Next(summaries.Length)]
            })
            .ToArray();
        }
    }
}
