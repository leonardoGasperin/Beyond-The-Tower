using btt.Aplicacao.Contrato;
using btt.Aplicacao.Servico;
using btt.Core.Contrato;
using btt.Core.Negocio;
using Reflex.Core;
using UnityEngine;

namespace btt.Aplicacao.DI
{
    /// <summary>
    /// Responsável por registrar as dependências da aplicação no contêiner de injeção de dependência.
    /// </summary>
    public class Installer : MonoBehaviour, IInstaller
    {
        /// <summary>
        /// Registra todas as dependências necessárias utilizando o <see cref="ContainerBuilder"/>.
        /// </summary>
        /// <param name="containerBuilder">Instância do construtor de contêiner para registrar os serviços.</param>
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            ServicosInternosDependencias(containerBuilder);
            NegocioDependencias(containerBuilder);
        }

        /// <summary>
        /// Registra os serviços internos como singletons no contêiner.
        /// </summary>
        /// <param name="builder">Construtor de contêiner para registro dos serviços.</param>
        private void ServicosInternosDependencias(ContainerBuilder builder)
        {
            builder.AddSingleton(new SingletonExemploServico(), typeof(ISingletonExemploServico));
        }

        /// <summary>
        /// Registra as dependências de negócio como singletons no contêiner.
        /// </summary>
        /// <param name="builder">Construtor de contêiner para registro das dependências de negócio.</param>
        private void NegocioDependencias(ContainerBuilder builder)
        {
            builder.AddSingleton(new SingletonExemploNegocio(), typeof(ISingletonExemploNegocio));
        }
    }
}
