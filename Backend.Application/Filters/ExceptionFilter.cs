using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace Backend.Application.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private ILogger<ExceptionFilter> _Logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _Logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var result = new ObjectResult(context.Exception.Message)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
            };

            _Logger.LogError(JsonSerializer.Serialize(result));
            
            context.Result = result;

            return;
        }
    }
}