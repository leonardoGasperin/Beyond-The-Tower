using btt.Aplicacao.Contrato;
using btt.Aplicacao.Servico;
using btt.Core.Contrato;
using btt.Core.Negocio;
using Reflex.Core;
using UnityEngine;

namespace btt.Aplicacao.DI.Core
{
    /// <summary>
    /// Respons�vel por registrar as depend�ncias da aplica��o no cont�iner de inje��o de depend�ncia.
    /// </summary>
    public class Installer : MonoBehaviour, IInstaller
    {
        /// <summary>
        /// Registra todas as depend�ncias necess�rias utilizando o <see cref="ContainerBuilder"/>.
        /// </summary>
        /// <param name="containerBuilder">Inst�ncia do construtor de cont�iner para registrar os servi�os.</param>
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            ServicosInternosDependencias(containerBuilder);
            NegocioDependencias(containerBuilder);
        }

        /// <summary>
        /// Registra os servi�os internos como singletons no cont�iner.
        /// </summary>
        /// <param name="builder">Construtor de cont�iner para registro dos servi�os.</param>
        private void ServicosInternosDependencias(ContainerBuilder builder)
        {
            builder.AddSingleton(new SingletonExemploServico(), typeof(ISingletonExemploServico));
        }

        /// <summary>
        /// Registra as depend�ncias de neg�cio como singletons no cont�iner.
        /// </summary>
        /// <param name="builder">Construtor de cont�iner para registro das depend�ncias de neg�cio.</param>
        private void NegocioDependencias(ContainerBuilder builder)
        {
            builder.AddSingleton(new SingletonExemploNegocio(), typeof(ISingletonExemploNegocio));
        }
    }
}
