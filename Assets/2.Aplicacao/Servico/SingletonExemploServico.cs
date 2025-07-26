using btt.Aplicacao.Contrato;
using UnityEngine;

namespace btt.Aplicacao.Servico
{
    /// <summary>
    /// Implementação do serviço de exemplo como singleton.
    /// Este serviço é destinado a ser injetado como uma dependência singleton,
    /// garantindo que apenas uma instância exista durante o ciclo de vida da aplicação.
    /// </summary>
    public class SingletonExemploServico : ISingletonExemploServico
    {
        /// <summary>
        /// Exemplo de método que demonstra a funcionalidade do serviço singleton.
        /// Escreve uma mensagem no console do Unity para indicar que o serviço foi injetado com sucesso.
        /// </summary>
        public void ExemploMetodo() => Debug.Log("[Controller] Serviço singleton injetado com sucesso!");
    }
}
