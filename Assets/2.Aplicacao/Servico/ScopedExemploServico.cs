using btt.Aplicacao.Contrato;
using System;
using UnityEngine;

namespace btt.Aplicacao.Servico
{
    /// <summary>
    /// Implementa��o de um servi�o de exemplo com escopo (scoped).
    /// Cada inst�ncia deste servi�o possui um identificador �nico (Guid) e � criada para cada escopo definido,
    /// garantindo isolamento entre diferentes ciclos de vida de entidades ou contextos.
    /// </summary>
    public class ScopedExemploServico : IScopedExemploServico
    {
        /// <summary>
        /// Identificador �nico da inst�ncia do servi�o, gerado no momento da cria��o.
        /// </summary>
        public Guid Id { get; } = Guid.NewGuid();

        /// <summary>
        /// M�todo de exemplo que demonstra a funcionalidade do servi�o scoped.
        /// Escreve uma mensagem no console do Unity indicando o sucesso da inje��o e o identificador da inst�ncia.
        /// </summary>
        public void ExemploMetodo() => Debug.Log("[Personagem] " + Id + " Servi�o Scoped injetado com sucesso!");
    }
}
