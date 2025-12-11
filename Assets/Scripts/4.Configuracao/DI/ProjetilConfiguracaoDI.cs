using btt.Aplicacao.Servico;
using btt.Aplicacao.Contratos;
using btt.Aplicacao.Contrato;
using Reflex.Core;
using UnityEngine;

namespace btt.Configuracao.DI.Projetil
{
    public class ProjetilConfiguracaoDI : MonoBehaviour
    {
        /// <summary>
        /// Propriedade que fornece acesso aos servi?os resolvidos para este escopo.
        /// </summary>
        public ServiceLocator Services { get; private set; }

        /// <summary>
        /// Inicializa o cont?iner de depend?ncias scoped e resolve os servi?os necess?rios ao acordar a entidade.
        /// </summary>
        void Awake()
        {
            var builder = new ContainerBuilder();
            builder.AddScoped(typeof(CombateServico), typeof(ICombateServico));
            builder.AddScoped(typeof(LancaServico), typeof(ILancaServico));
            var container = builder.Build();

            Services = new ServiceLocator(container);
        }

        /// <summary>
        /// Service Locator que exp?e as inst?ncias dos servi?os scoped resolvidos para este escopo.
        /// </summary>
        public class ServiceLocator
        {
            /// <summary>
            /// Servi?o de exemplo com escopo, implementando <see cref="ImovimentacaoServico"/>.
            /// </summary>
            public ICombateServico combateServico;
            public ILancaServico lancaServico;
            // Adicione todos os outros servi?os como campos

            /// <summary>
            /// Resolve e armazena as inst?ncias dos servi?os scoped a partir do cont?iner fornecido.
            /// </summary>
            /// <param name="container">Cont?iner de depend?ncias scoped.</param>
            public ServiceLocator(Container container)
            {
                combateServico = (ICombateServico)container.Resolve(typeof(ICombateServico));
                lancaServico = (ILancaServico)container.Resolve(typeof(ILancaServico));

                // Resolva todos os outros servi?os aqui...
            }
        }
    }
}
