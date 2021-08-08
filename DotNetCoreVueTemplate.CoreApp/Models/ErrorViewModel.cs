using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;

namespace DotNetCoreVueTemplate.CoreApp.Models
{
    public class ErrorViewModel
    {
        public ErrorViewModel()
        {

        }

        public ErrorViewModel(ExceptionContext context)
        {
            Controller = context.RouteData?.Values?["controller"].ToString();
            Path = context.HttpContext.Request.Path;
            QueryString = context.HttpContext.Request.QueryString;
            Exception = context.Exception;
        }

        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public Guid ErrorId { get; } = Guid.NewGuid();

        public string Controller { get; set; }
        public string Path { get; set; }
        public QueryString QueryString { get; set; }
        public Exception  Exception { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public string ToString(JsonSerializerSettings jsonSerializerSettings)
        {
            return JsonConvert.SerializeObject(this, jsonSerializerSettings);
        }
    }
}
