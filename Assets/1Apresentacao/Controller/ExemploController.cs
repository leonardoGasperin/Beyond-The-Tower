using btt.Aplicacao.Contrato;
using btt.Core.Contrato;
using Reflex.Attributes;
using UnityEngine;

/// <summary>
/// Controlador de exemplo respons�vel por demonstrar a inje��o e utiliza��o de servi�os singleton.
/// Este controlador injeta e utiliza inst�ncias de <see cref="ISingletonExemploServico"/> e <see cref="ISingletonExemploNegocio"/>,
/// chamando seus m�todos de exemplo ao iniciar.
/// </summary>
public class ExemploController : MonoBehaviour
{
    /// <summary>
    /// Servi�o singleton de exemplo injetado pelo cont�iner de depend�ncias.
    /// </summary>
    [Inject] protected ISingletonExemploServico singletonExemploServico;

    /// <summary>
    /// Neg�cio singleton de exemplo injetado pelo cont�iner de depend�ncias.
    /// </summary>
    [Inject] protected ISingletonExemploNegocio singletonExemploNegocio;

    /// <summary>
    /// M�todo chamado na inicializa��o do MonoBehaviour.
    /// Executa os m�todos de exemplo dos servi�os injetados.
    /// </summary>
    public void Start()
    {
        singletonExemploServico.ExemploMetodo();
        singletonExemploNegocio.ExemploMetodo();
    }
}
