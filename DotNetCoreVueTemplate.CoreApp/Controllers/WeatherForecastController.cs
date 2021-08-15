using AutoMapper;
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
    [ApiController]
    [Route("api/[controller]")]
    [ServiceFilter(typeof(HandleExceptionFilter))] 
    public class WeatherForecastController : ControllerBase
    {
        private readonly DotnetCoreVueTemplateContext context;
        private readonly ILogger<WeatherForecastController> logger;
        private readonly IMapper mapper;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, DotnetCoreVueTemplateContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<Models.WeatherForecast> Get()
        {
            var tenDaysAgo = DateTime.Now - TimeSpan.FromDays(10);
            return from table in this.context.WeatherForecasts
                   where table.Date > tenDaysAgo
                   select mapper.Map<Models.WeatherForecast>(table);
        }
    }
}
