using btt.Aplicacao.Contrato;
using btt.Core.Contrato;
using Reflex.Attributes;
using UnityEngine;

namespace btt.Core.Entidade
{
    public class Personagem : MonoBehaviour
    {
        [Inject] protected IScopedExemploServico scopedExemploServico;
        [Inject] protected IScopedExemploNegocio scopedExemploNegocio;

        public virtual void Start()
        {
            scopedExemploServico.ExemploMetodo();
            scopedExemploNegocio.ExemploMetodo();
        }
    }
}
