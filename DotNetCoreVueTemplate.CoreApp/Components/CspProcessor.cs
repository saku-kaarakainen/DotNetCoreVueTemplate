using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreVueTemplate.CoreApp.Components
{
    public class CspProcessor
    {
        private readonly IConfiguration config;

        public CspProcessor(IConfiguration configuration)
        {
            config = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            List<(string, string)> headers = new ()
            {
                ("Content-Security-Policy", config.GetValue<string>("CSP-Headers:CSP")),
                ("X-Frame-Options", config.GetValue<string>("CSP-Headers:X-Frame-Options"))
            };


            foreach (var hdr in headers)
            {
                if (!context.Response.Headers.ContainsKey(hdr.Item1))
                {
                    context.Response.Headers.Add(hdr.Item1, hdr.Item2);
                }
                else
                {
                    context.Response.Headers[hdr.Item1] = hdr.Item2;
                }
            }
        }
    }
}
