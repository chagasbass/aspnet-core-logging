using EffectiveLogging.Extensions.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace EffectiveLogging.Extensions.ServicesExtensions
{
    public static class InfoMachineExtensions
    {
        public static void RecuperarInformacoesDeMaquina(this IServiceCollection services) {
            services.AddSingleton<IScopeInformation, ScopeInformation>();
        }
    }
}
