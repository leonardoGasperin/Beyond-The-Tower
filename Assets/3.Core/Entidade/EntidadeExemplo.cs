using btt.Aplicacao.DI.Exemplo;
using UnityEngine;

namespace btt.Core.Entidade
{
    /// <summary>
    /// Exemplo de personagem que utiliza servi�os com escopo (scoped) resolvidos via Service Locator.
    /// Esta classe demonstra como acessar e utilizar servi�os e neg�cios scoped, 
    /// que s�o criados e gerenciados para o ciclo de vida da entidade associada.
    /// </summary>
    public class PersonagemExemplo : MonoBehaviour
    {
        /// <summary>
        /// Service Locator que fornece acesso aos servi�os scoped desta entidade.
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
        /// Executa m�todos de exemplo dos servi�os scoped ao iniciar a entidade.
        /// </summary>
        public virtual void Start()
        {
            services.scopedExemploServico.ExemploMetodo();
            services.scopedExemploNegocio.ExemploMetodo();
            // E assim por diante...
        }
    }
}
