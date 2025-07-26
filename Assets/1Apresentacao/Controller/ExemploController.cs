using btt.Aplicacao.Contrato;
using btt.Core.Contrato;
using Reflex.Attributes;
using UnityEngine;

/// <summary>
/// Controlador de exemplo responsável por demonstrar a injeção e utilização de serviços singleton.
/// Este controlador injeta e utiliza instâncias de <see cref="ISingletonExemploServico"/> e <see cref="ISingletonExemploNegocio"/>,
/// chamando seus métodos de exemplo ao iniciar.
/// </summary>
public class ExemploController : MonoBehaviour
{
    /// <summary>
    /// Serviço singleton de exemplo injetado pelo contêiner de dependências.
    /// </summary>
    [Inject] protected ISingletonExemploServico singletonExemploServico;

    /// <summary>
    /// Negócio singleton de exemplo injetado pelo contêiner de dependências.
    /// </summary>
    [Inject] protected ISingletonExemploNegocio singletonExemploNegocio;

    /// <summary>
    /// Método chamado na inicialização do MonoBehaviour.
    /// Executa os métodos de exemplo dos serviços injetados.
    /// </summary>
    public void Start()
    {
        singletonExemploServico.ExemploMetodo();
        singletonExemploNegocio.ExemploMetodo();
    }
}
