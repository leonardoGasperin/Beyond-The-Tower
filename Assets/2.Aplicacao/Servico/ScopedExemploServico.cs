using btt.Aplicacao.Contrato;
using UnityEngine;

namespace btt.Aplicacao.Servico
{
    public class ScopedExemploServico : IScopedExemploServico
    {
        public void ExemploMetodo() => Debug.Log("[Personagem] Serviço Scoped injetado com sucesso!");
    }
}
