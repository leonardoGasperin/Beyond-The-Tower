using btt.Core.Contrato;
using System;
using UnityEngine;

namespace btt.Core.Negocio
{
    /// <summary>
    /// Neg�cio que implementa a interface IScopedExemploNegocio.
    /// </summary>
    public class ScopedExemploNegocio : IScopedExemploNegocio
    {
        public Guid Id { get; } = Guid.NewGuid();
        /// <summary>
        /// M�todo de exemplo que pode ser implementado pelo neg�cio.
        /// </summary>
        public void ExemploMetodo() => Debug.Log("[Personagem] " + Id + " Neg�cio scoped injetado com sucesso!");
    }
}
