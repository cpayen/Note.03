using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace Note.Api.Middlewares
{
    class LoggingMiddleware
    {
        protected readonly RequestDelegate _next;
        protected readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var sw = Stopwatch.StartNew();

            try
            {
                await _next(context);

                sw.Stop();
                var ellapsed = sw.Elapsed.TotalMilliseconds;

                HandleLogging(context, ellapsed, _logger);
            }
            catch (Exception exception)
            {
                sw.Stop();
                var ellapsed = sw.Elapsed.TotalMilliseconds;

                HandleLogging(context, ellapsed, _logger, exception);
            }
        }

        private static void HandleLogging(HttpContext context, double ellapsed, ILogger<LoggingMiddleware> logger, Exception exception = null)
        {
            var code = context.Response.StatusCode;
            var method = context.Request.Method;
            var path = context.Request.Path;
            var user = context.User.Identity.Name ?? "Anonymous";

            var log = $"{code} - HTTP {method} {path} in {ellapsed:0.0000} ms (User: {user})";

            switch (code)
            {
                case (int)HttpStatusCode.OK:
                    logger.LogInformation(log);
                    break;

                case (int)HttpStatusCode.InternalServerError:
                    logger.LogCritical(log);
                    if(exception != null)
                    {
                        logger.LogCritical(exception, "Exception");
                    }
                    break;

                case (int)HttpStatusCode.NotFound:
                    logger.LogError(log);
                    if(exception != null)
                    {
                        logger.LogError(exception, "NotFound");
                    }
                    break;

                default:
                    logger.LogError(log);
                    if (exception != null)
                    {
                        logger.LogError(exception, "Error");
                    }
                    break;
            }
        }
    }
}