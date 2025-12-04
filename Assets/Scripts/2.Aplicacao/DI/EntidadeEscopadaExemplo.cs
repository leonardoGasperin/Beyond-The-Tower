using btt.Aplicacao.Contrato;
using btt.Aplicacao.Servico;
using btt.Core.Contrato;
using btt.Core.Negocio;
using Reflex.Core;
using UnityEngine;

    namespace btt.Aplicacao.DI.Exemplo{
    /// <summary>
    /// Exemplo de entidade que demonstra a configura��o e utiliza��o de servi�os com escopo (scoped) via inje��o de depend�ncia.
    /// Cria um cont�iner de depend�ncias para o ciclo de vida desta entidade e exp�e os servi�os resolvidos atrav�s de um Service Locator.
    /// </summary>
    public class EntidadeEscopadaExemplo : MonoBehaviour
    {
        /// <summary>
        /// Propriedade que fornece acesso aos servi�os resolvidos para este escopo.
        /// </summary>
        public ServiceLocator Services { get; private set; }

        /// <summary>
        /// Inicializa o cont�iner de depend�ncias scoped e resolve os servi�os necess�rios ao acordar a entidade.
        /// </summary>
        void Awake()
        {
            var builder = new ContainerBuilder();
            builder.AddScoped(typeof(ScopedExemploServico), typeof(IScopedExemploServico));
            builder.AddScoped(typeof(ScopedExemploNegocio), typeof(IScopedExemploNegocio));
            var container = builder.Build();

            Services = new ServiceLocator(container);
        }

        /// <summary>
        /// Service Locator que exp�e as inst�ncias dos servi�os scoped resolvidos para este escopo.
        /// </summary>
        public class ServiceLocator
        {
            /// <summary>
            /// Servi�o de exemplo com escopo, implementando <see cref="IScopedExemploServico"/>.
            /// </summary>
            public IScopedExemploServico scopedExemploServico;

            /// <summary>
            /// Neg�cio de exemplo com escopo, implementando <see cref="IScopedExemploNegocio"/>.
            /// </summary>
            public IScopedExemploNegocio scopedExemploNegocio;
            // Adicione todos os outros servi�os como campos

            /// <summary>
            /// Resolve e armazena as inst�ncias dos servi�os scoped a partir do cont�iner fornecido.
            /// </summary>
            /// <param name="container">Cont�iner de depend�ncias scoped.</param>
            public ServiceLocator(Container container)
            {
                scopedExemploServico = (IScopedExemploServico)container.Resolve(typeof(IScopedExemploServico));
                scopedExemploNegocio = (IScopedExemploNegocio)container.Resolve(typeof(IScopedExemploNegocio));
                // Resolva todos os outros servi�os aqui...
            }
        }
    }
}