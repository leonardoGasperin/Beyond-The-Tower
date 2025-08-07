using UnityEngine;

namespace btt.Core.Entidade
{
    /// <summary>
    /// Exemplo de personagem que utiliza serviços com escopo (scoped) resolvidos via Service Locator.
    /// Esta classe demonstra como acessar e utilizar serviços e negócios scoped, 
    /// que são criados e gerenciados para o ciclo de vida da entidade associada.
    /// </summary>
    public class PersonagemExemplo : MonoBehaviour
    {
        /// <summary>
        /// Service Locator que fornece acesso aos serviços scoped desta entidade.
        /// </summary>
        protected EntidadeEscopadaExemplo.ServiceLocator services;

        /// <summary>
        /// Inicializa o Service Locator ao acordar a entidade, obtendo-o do componente associado.
        /// </summary>
        public virtual void Awake()
        {
            services = GetComponent<EntidadeEscopadaExemplo>().Services;
        }

        /// <summary>
        /// Executa métodos de exemplo dos serviços scoped ao iniciar a entidade.
        /// </summary>
        public virtual void Start()
        {
            services.scopedExemploServico.ExemploMetodo();
            services.scopedExemploNegocio.ExemploMetodo();
            // E assim por diante...
        }
    }
}
