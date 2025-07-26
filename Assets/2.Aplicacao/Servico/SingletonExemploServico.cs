using btt.Aplicacao.Contrato;
using UnityEngine;

namespace btt.Aplicacao.Servico
{
    /// <summary>
    /// Implementa��o do servi�o de exemplo como singleton.
    /// Este servi�o � destinado a ser injetado como uma depend�ncia singleton,
    /// garantindo que apenas uma inst�ncia exista durante o ciclo de vida da aplica��o.
    /// </summary>
    public class SingletonExemploServico : ISingletonExemploServico
    {
        /// <summary>
        /// Exemplo de m�todo que demonstra a funcionalidade do servi�o singleton.
        /// Escreve uma mensagem no console do Unity para indicar que o servi�o foi injetado com sucesso.
        /// </summary>
        public void ExemploMetodo() => Debug.Log("[Controller] Servi�o singleton injetado com sucesso!");
    }
}
