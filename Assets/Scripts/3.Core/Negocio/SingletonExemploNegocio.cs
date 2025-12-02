using btt.Core.Contrato;
using UnityEngine;

namespace btt.Core.Negocio
{
    public class SingletonExemploNegocio : ISingletonExemploNegocio
    {
        public void ExemploMetodo() => Debug.Log("[Controller] Negocio singleton injetado com sucesso!");
    }
}
