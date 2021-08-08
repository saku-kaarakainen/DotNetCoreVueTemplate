using DotNetCoreVueTemplate.CoreApp.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoreVueTemplate.CoreApp.Components
{
    /// <summary>
    /// Handles the exceptions when a such is risen in the controller that has this attribute
    /// </summary>
    public class HandleExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<HandleExceptionFilter> logger;

        public HandleExceptionFilter(ILogger<HandleExceptionFilter> logger)
        {
            this.logger = logger;
        }

        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            ErrorViewModel error = new(context);
            JsonSerializerSettings jsonSerializerSettings = new()
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            };

            var json = error.ToString(jsonSerializerSettings);

            // Logging is done in try-catch in order not to cause more errors.
            try
            {
                this.logger.LogError("An unexpected error occured. Details: {Details}", json);
            }
            catch { }

            // Write the error to the response
            var bytes = Encoding.UTF8.GetBytes(json);
            await context.HttpContext.Response.Body.WriteAsync(bytes.AsMemory(0, bytes.Length));
        }

    }
}
