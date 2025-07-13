using btt.Aplicacao.Contrato;
using btt.Aplicacao.Servico;
using btt.Core.Contrato;
using btt.Core.Negocio;
using Reflex.Core;
using Reflex.Injectors;
using UnityEngine;

public class EntidadeEscopadaExemplo : MonoBehaviour, IInstaller
{
    public void InstallBindings(ContainerBuilder containerBuilder)
    {
        ServicosInternosDependencias(containerBuilder);
        NegocioDependencias(containerBuilder);
    }

    private void ServicosInternosDependencias(ContainerBuilder builder)
    {
        builder.AddScoped(typeof(ScopedExemploServico), typeof(IScopedExemploServico));
    }

    private void NegocioDependencias(ContainerBuilder builder)
    {
        builder.AddScoped(typeof(ScopedExemploNegocio), typeof(IScopedExemploNegocio));
    }
}
