using btt.Aplicacao.Contrato;
using System;
using UnityEngine;

namespace btt.Aplicacao.Servico
{
    public class ScopedExemploServico : IScopedExemploServico
    {
        public void ExemploMetodo() => Debug.Log("[Personagem] Servi�o Scoped injetado com sucesso!");
    }
}
