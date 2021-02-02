using Microsoft.AspNetCore.Builder;
using System;

namespace EffectiveLogging.Middlewares.Middlewares
{
    /// <summary>
    /// Extensoes do Middleware
    /// </summary>
    public static class ApiExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseApiExceptionHandler(this IApplicationBuilder builder)
        {
            var options = new ApiExceptionOptions();
            return builder.UseMiddleware<ApiExceptionMiddleware>(options);
        }

        public static IApplicationBuilder UseApiExceptionHandler(this IApplicationBuilder builder,
            Action<ApiExceptionOptions> configureOptions)
        {
            var options = new ApiExceptionOptions();
            configureOptions(options);

            return builder.UseMiddleware<ApiExceptionMiddleware>(options);
        }
    }
}
