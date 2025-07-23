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
        }

        private void NegocioDependencias(ContainerBuilder builder)
        {
            builder.AddSingleton(new SingletonExemploNegocio(), typeof(ISingletonExemploNegocio));
        }
    }
}
