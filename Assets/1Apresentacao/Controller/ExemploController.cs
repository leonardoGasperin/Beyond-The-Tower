using btt.Aplicacao.Contrato;
using btt.Core.Contrato;
using Reflex.Attributes;
using UnityEngine;

public class ExemploController : MonoBehaviour
{
    [Inject] protected ISingletonExemploServico singletonExemploServico;
    [Inject] protected ISingletonExemploNegocio singletonExemploNegocio;

    public void Start()
    {
        singletonExemploServico.ExemploMetodo();
        singletonExemploNegocio.ExemploMetodo();
    }
}
