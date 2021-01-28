using Microsoft.Extensions.Logging;
using System;

namespace EffectiveLogging.Middlewares.Entities
{
    public static class LoggerDefines
    {
        private static  Action<ILogger, Exception> _acaoUm;
        private static  Action<ILogger, string, Exception> _acaoDois;
        private static  Func<ILogger, string, IDisposable> _acaoTres;

        static LoggerDefines()
        {
            _acaoUm = LoggerMessage.Define(LogLevel.Information, 0, "Dentro do Controller recuperando dados");

            _acaoDois = LoggerMessage.Define<string>(LogLevel.Debug, 1, "Debugando!");

            _acaoTres = LoggerMessage.DefineScope<string>("Debugando outro!");
        }

        public static void EfetuarAcaoUm(this ILogger logger)
        {
            _acaoUm(logger, null);
        }

        public static void EfetuarAcaoDois(this ILogger logger, string dado)
        {
            _acaoDois(logger, dado, null);
        }

        public static IDisposable EfetuarAcaoTres(this ILogger logger, string dado)
        {
            return _acaoTres(logger, dado);
        }

    }
}
