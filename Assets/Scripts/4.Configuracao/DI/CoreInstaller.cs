using Reflex.Core;
using UnityEngine;

namespace btt.Configuracao.DI.Core
{
    /// <summary>
    /// Respons?vel por registrar as depend?ncias da aplica??o no cont?iner de inje??o de depend?ncia.
    /// </summary>
    public class Installer : MonoBehaviour, IInstaller
    {
        /// <summary>
        /// Registra todas as depend?ncias necess?rias utilizando o <see cref="ContainerBuilder"/>.
        /// </summary>
        /// <param name="containerBuilder">Inst?ncia do construtor de cont?iner para registrar os servi?os.</param>
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            ServicosInternosDependencias(containerBuilder);
        }

        /// <summary>
        /// Registra os servi?os internos como singletons no cont?iner.
        /// </summary>
        /// <param name="builder">Construtor de cont?iner para registro dos servi?os.</param>
        private void ServicosInternosDependencias(ContainerBuilder builder)
        {
        }
    }
}