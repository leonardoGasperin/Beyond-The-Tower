using btt.Aplicacao.Servico;
using btt.Aplicacao.Contratos;
using btt.Core.Contrato;
using Reflex.Core;
using UnityEngine;

namespace btt.Aplicacao.DI.Personagem {
    public class PersonagemConfiguracaoDI : MonoBehaviour
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
            builder.AddScoped(typeof(MovimentacaoServico), typeof(IMovimentacaoServico));
            var container = builder.Build();

            Services = new ServiceLocator(container);
        }

        /// <summary>
        /// Service Locator que exp�e as inst�ncias dos servi�os scoped resolvidos para este escopo.
        /// </summary>
        public class ServiceLocator
        {
            /// <summary>
            /// Servi�o de exemplo com escopo, implementando <see cref="ImovimentacaoServico"/>.
            /// </summary>
            public IMovimentacaoServico movimentacaoServico;
            // Adicione todos os outros servi�os como campos

            /// <summary>
            /// Resolve e armazena as inst�ncias dos servi�os scoped a partir do cont�iner fornecido.
            /// </summary>
            /// <param name="container">Cont�iner de depend�ncias scoped.</param>
            public ServiceLocator(Container container)
            {
                movimentacaoServico = (IMovimentacaoServico)container.Resolve(typeof(IMovimentacaoServico));
                // Resolva todos os outros servi�os aqui...
            }
        }
    }
}
