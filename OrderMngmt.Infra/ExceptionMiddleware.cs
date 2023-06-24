using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using OrderMngmt.Infra.Exceptions;

namespace OrderMngmt.Infra
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(
            RequestDelegate next, 
            ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(
            HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(
                    context, 
                    ex);
            }
        }

        private static Task HandleExceptionAsync(
            HttpContext context, 
            Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;

            if (exception is NotFoundException)
            {
                code = HttpStatusCode.NotFound;
            }
            else if (exception is BadRequestException)
            {
                code = HttpStatusCode.BadRequest;
            }

            var result = JsonConvert.SerializeObject(
                new { error = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}