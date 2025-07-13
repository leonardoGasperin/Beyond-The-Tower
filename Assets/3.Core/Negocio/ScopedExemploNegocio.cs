using btt.Core.Contrato;
using UnityEngine;

namespace btt.Core.Negocio
{
    /// <summary>
    /// Negócio que implementa a interface IScopedExemploNegocio.
    /// </summary>
    public class ScopedExemploNegocio : IScopedExemploNegocio
    {
        /// <summary>
        /// Método de exemplo que pode ser implementado pelo negócio.
        /// </summary>
        public void ExemploMetodo() => Debug.Log("[Personagem] Negócio scoped injetado com sucesso!");
    }
}
