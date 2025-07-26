using btt.Aplicacao.Contrato;
using btt.Aplicacao.Servico;
using btt.Core.Contrato;
using btt.Core.Negocio;
using Reflex.Core;
using UnityEngine;

/// <summary>
/// Exemplo de entidade que demonstra a configuração e utilização de serviços com escopo (scoped) via injeção de dependência.
/// Cria um contêiner de dependências para o ciclo de vida desta entidade e expõe os serviços resolvidos através de um Service Locator.
/// </summary>
public class EntidadeEscopadaExemplo : MonoBehaviour
{
    /// <summary>
    /// Propriedade que fornece acesso aos serviços resolvidos para este escopo.
    /// </summary>
    public ServiceLocator Services { get; private set; }

    /// <summary>
    /// Inicializa o contêiner de dependências scoped e resolve os serviços necessários ao acordar a entidade.
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
    /// Service Locator que expõe as instâncias dos serviços scoped resolvidos para este escopo.
    /// </summary>
    public class ServiceLocator
    {
        /// <summary>
        /// Serviço de exemplo com escopo, implementando <see cref="IScopedExemploServico"/>.
        /// </summary>
        public IScopedExemploServico scopedExemploServico;

        /// <summary>
        /// Negócio de exemplo com escopo, implementando <see cref="IScopedExemploNegocio"/>.
        /// </summary>
        public IScopedExemploNegocio scopedExemploNegocio;
        // Adicione todos os outros serviços como campos

        /// <summary>
        /// Resolve e armazena as instâncias dos serviços scoped a partir do contêiner fornecido.
        /// </summary>
        /// <param name="container">Contêiner de dependências scoped.</param>
        public ServiceLocator(Container container)
        {
            scopedExemploServico = (IScopedExemploServico)container.Resolve(typeof(IScopedExemploServico));
            scopedExemploNegocio = (IScopedExemploNegocio)container.Resolve(typeof(IScopedExemploNegocio));
            // Resolva todos os outros serviços aqui...
        }
    }
}
