using btt.Core.Contrato;
using System;
using UnityEngine;

namespace btt.Core.Negocio
{
    /// <summary>
    /// Negócio que implementa a interface IScopedExemploNegocio.
    /// </summary>
    public class ScopedExemploNegocio : IScopedExemploNegocio
    {
        public Guid Id { get; } = Guid.NewGuid();
        /// <summary>
        /// Método de exemplo que pode ser implementado pelo negócio.
        /// </summary>
        public void ExemploMetodo() => Debug.Log("[Personagem] " + Id + " Negócio scoped injetado com sucesso!");
    }
}
