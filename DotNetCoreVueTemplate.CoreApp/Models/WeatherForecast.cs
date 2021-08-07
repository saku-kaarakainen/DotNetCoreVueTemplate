using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreVueTemplate.CoreApp.Models
{
    // 2021-08-07: Class copied from: https://github.com/SoftwareAteliers/asp-net-core-vue-starter/blob/master/Models/WeatherForecast.cs
    // Credits to SoftwareAteliers 
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }
}
