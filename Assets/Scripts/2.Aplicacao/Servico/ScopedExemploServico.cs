using btt.Aplicacao.Contrato;
using System;
using UnityEngine;

namespace btt.Aplicacao.Servico
{
    /// <summary>
    /// Implementação de um serviço de exemplo com escopo (scoped).
    /// Cada instância deste serviço possui um identificador único (Guid) e é criada para cada escopo definido,
    /// garantindo isolamento entre diferentes ciclos de vida de entidades ou contextos.
    /// </summary>
    public class ScopedExemploServico : IScopedExemploServico
    {
        /// <summary>
        /// Identificador único da instância do serviço, gerado no momento da criação.
        /// </summary>
        public Guid Id { get; } = Guid.NewGuid();

        /// <summary>
        /// Método de exemplo que demonstra a funcionalidade do serviço scoped.
        /// Escreve uma mensagem no console do Unity indicando o sucesso da injeção e o identificador da instância.
        /// </summary>
        public void ExemploMetodo() => Debug.Log("[Personagem] " + Id + " Serviço Scoped injetado com sucesso!");
    }
}
