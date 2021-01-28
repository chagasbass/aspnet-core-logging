using EffectiveLogging.Extensions.Entities;
using EffectiveLogging.Middlewares.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace EffectiveLogging.Middlewares.Middlewares
{
    /// <summary>
    /// Middleware de tratamento global de Exceptions
    /// </summary>
    public class ApiExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiExceptionMiddleware> _logger;
        private readonly ApiExceptionOptions _options;
        private readonly IScopeInformation _scopeInformation;

        public ApiExceptionMiddleware(RequestDelegate next,
                                      ILogger<ApiExceptionMiddleware> logger,
                                      ApiExceptionOptions options,
                                      IScopeInformation scopeInformation)
        {
            _next = next;
            _logger = logger;
            _options = options;
            _scopeInformation = scopeInformation;
        }


        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                using (_logger.BeginScope(_scopeInformation.HostScopeInfo))
                {
                    _logger.LogError("Erro na aplicação");

                    await HandleExceptionAsync(context, ex, _options);
                }
            }
            finally
            {
                LogRequestInformation(context);
            }
        }

        /// <summary>
        /// Fluxo que controla as exceptions geradas na aplicação
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        private Task HandleExceptionAsync(HttpContext context, Exception exception, ApiExceptionOptions options)
        {
            var statusCode = GetHttpStatusCodeByException(exception);

            var problemDetail = new ProblemDetail()
            {
                Id = Guid.NewGuid().ToString(),
                CodigoHttp = statusCode,
                Titulo = "Erro ocorrido na Api"
            };

            options.AddResponseDetails?.Invoke(context, exception, problemDetail);

            var innerException = GetInnerExceptionMessage(exception);

            var level = options.DetermineLogLevel?.Invoke(exception) ?? LogLevel.Error;

            _logger.Log(level, exception, $"BAD ERROR!! {innerException} -- {problemDetail.Id}");

            var result = JsonSerializer.Serialize(problemDetail);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(result);
        }

        /// <summary>
        /// Cria o log das informações de request
        /// </summary>
        /// <param name="context"></param>
        private void LogRequestInformation(HttpContext context)
        {
            _logger.LogInformation(
                "Request {method} {url} => {statusCode}",
                context.Request?.Method,
                context.Request?.Path.Value,
                context.Response?.StatusCode);
        }

        /// <summary>
        /// Retorna um HttpStatusCode de acordo com a Exception Gerada
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        private int GetHttpStatusCodeByException(Exception exception)
        {
            if (exception is Exception)
                return (int)HttpStatusCode.BadRequest;
            if (exception is NotImplementedException)
                return (int)HttpStatusCode.NotFound;
            else
                return (int)HttpStatusCode.InternalServerError;
        }

        /// <summary>
        /// Recupera as mensagens de uma Exception ou de uma InnerException caso exista
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        private string GetInnerExceptionMessage(Exception exception)
        {
            if (exception.InnerException != null)
                return GetInnerExceptionMessage(exception.InnerException);

            return exception.Message;
        }
    }
}