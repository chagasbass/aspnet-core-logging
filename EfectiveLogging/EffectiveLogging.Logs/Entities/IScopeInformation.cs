using System;
using System.Collections.Generic;
using System.Reflection;

namespace EffectiveLogging.Extensions.Entities
{
    /// <summary>
    /// Interface para busca de dados de maquina e entrada
    /// </summary>
    public interface IScopeInformation
    {
        Dictionary<string,string> HostScopeInfo { get; }
    }

    /// <summary>
    /// IMplementacao para busca de maquina e entradas
    /// </summary>
    public class ScopeInformation : IScopeInformation
    {
        public Dictionary<string, string> HostScopeInfo { get; }

        public ScopeInformation()
        {
            HostScopeInfo = new Dictionary<string, string>
            {
                {"MachineName",Environment.MachineName },
                {"EntryPoint",Assembly.GetEntryAssembly().GetName().Name }
            };
        }
    }
}
