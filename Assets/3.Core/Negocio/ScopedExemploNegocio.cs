using btt.Core.Contrato;
using UnityEngine;

namespace btt.Core.Negocio
{
    /// <summary>
    /// Neg�cio que implementa a interface IScopedExemploNegocio.
    /// </summary>
    public class ScopedExemploNegocio : IScopedExemploNegocio
    {
        /// <summary>
        /// M�todo de exemplo que pode ser implementado pelo neg�cio.
        /// </summary>
        public void ExemploMetodo() => Debug.Log("[Personagem] Neg�cio scoped injetado com sucesso!");
    }
}
