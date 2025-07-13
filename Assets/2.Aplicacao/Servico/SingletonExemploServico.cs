using btt.Aplicacao.Contrato;
using UnityEngine;

namespace btt.Aplicacao.Servico
{
    public class SingletonExemploServico : ISingletonExemploServico
    {
        public void ExemploMetodo() => Debug.Log("[Controller] Serviço singleton injetado com sucesso!");
    }
}
