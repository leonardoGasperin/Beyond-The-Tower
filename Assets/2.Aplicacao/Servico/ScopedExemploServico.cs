using btt.Aplicacao.Contrato;
using UnityEngine;

namespace btt.Aplicacao.Servico
{
    public class ScopedExemploServico : IScopedExemploServico
    {
        public void ExemploMetodo() => Debug.Log("[Personagem] Servi�o Scoped injetado com sucesso!");
    }
}
