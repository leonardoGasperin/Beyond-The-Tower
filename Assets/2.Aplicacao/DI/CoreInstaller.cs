using btt.Aplicacao.Contrato;
using btt.Aplicacao.Servico;
using btt.Core.Contrato;
using btt.Core.Negocio;
using Reflex.Core;
using UnityEngine;

namespace btt.Aplicacao.DI
{
    public class Installer : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            ServicosInternosDependencias(containerBuilder);
            NegocioDependencias(containerBuilder);
        }

        private void ServicosInternosDependencias(ContainerBuilder builder)
        {
            builder.AddSingleton(new SingletonExemploServico(), typeof(ISingletonExemploServico));
            //builder.AddScoped(typeof(ScopedExemploServico), typeof(IScopedExemploServico));
        }

        private void NegocioDependencias(ContainerBuilder builder)
        {
            builder.AddSingleton(new SingletonExemploNegocio(), typeof(ISingletonExemploNegocio));
            //builder.AddScoped(typeof(ScopedExemploNegocio), typeof(IScopedExemploNegocio));
        }
    }
}
