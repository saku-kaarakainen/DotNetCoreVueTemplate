using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreVueTemplate.CoreApp.Models
{
    public record WeatherForecast(
    DateTime Date,
    int TemperatureC,
    string Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
    /*
        public class WeatherForecast
        {
            public DateTime Date { get; set; }

            public int TemperatureC { get; set; }

            public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

            public string Summary { get; set; }
        }
    */
}
