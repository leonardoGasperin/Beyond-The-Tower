using btt.Aplicacao.Contrato;
using UnityEngine;

namespace btt.Aplicacao.Servico
{
    public class SingletonExemploServico : ISingletonExemploServico
    {
        public void ExemploMetodo() => Debug.Log("[Controller] Servi�o singleton injetado com sucesso!");
    }
}
