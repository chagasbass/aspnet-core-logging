using EffectiveLogging.Middlewares.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Data.SqlClient;

namespace EffectiveLogging.Middlewares.Middlewares
{
    /// <summary>
    /// Extensao para verificar tipos de exceptions e level de logs
    /// </summary>
    public static class ApiErrorResponseExtension
    {
        public static void UpdateApiErrorResponse(HttpContext context, Exception exception, ProblemDetail problemDetail)
        {
            if (exception.GetType().Name == typeof(SqlException).Name)
            {
                problemDetail.Detalhe = "Exception de Banco de dados";
            }
            if (exception.GetType().Name == typeof(Exception).Name)
            {
                problemDetail.Detalhe = "Exception de fluxo";
            }
        }

        public static LogLevel DetermineLogLevel(Exception exception)
        {
            //verificando se existe erro de conexao com o banco ou rede
            if (exception.Message.StartsWith("cannot open database", StringComparison.InvariantCultureIgnoreCase) ||
                exception.Message.StartsWith("a network-related", StringComparison.InvariantCultureIgnoreCase))
            {
                return LogLevel.Critical;
            }

            return LogLevel.Error;
        }
    }
}
