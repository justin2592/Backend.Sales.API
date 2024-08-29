using Backend.Sales.Application.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Sales.Application.Services.MiddleWare
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IApiKeyService _apiKeyService;

        public ApiKeyMiddleware(RequestDelegate next, IApiKeyService apiKeyService)
        {
            _next = next;
            _apiKeyService = apiKeyService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var userAgent = context.Request.Headers["User-Agent"].ToString();

            if (userAgent.Contains("swagger", StringComparison.OrdinalIgnoreCase))
            {
                // Logic specific to Swagger
                // For example, allow requests without an API key or with a hardcoded key for testing
                await _next(context);
                return;
            }

            if (context.Request.Headers.TryGetValue("SMS-API-KEY", out var apiKeyHeader))
            {
                var apiKey = apiKeyHeader.FirstOrDefault();

                if (_apiKeyService.ValidateApiKey(apiKey!))
                {
                    // You can fetch and set the user details based on the API key here if needed
                    context.Response.StatusCode = StatusCodes.Status200OK;
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Invalid or expired API key.");
                    return;
                }
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("API key is required.");
                return;
            }

            await _next(context);
        }
    }
}
