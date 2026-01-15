using UnityEngine;

namespace btt.Core.Fabrica
{
    public static class VetorTransmissaoFabrica
    {
        public static RaycastHit2D CriarVetorTransmissaoServico(Vector2 origen, Vector2 direcao, float distancia, int mascaraCamada)
            => Physics2D.Raycast(origen, direcao, distancia, mascaraCamada);

        public static RaycastHit2D CriarVetorTransmissaoServicoDebug(Vector2 origen, Vector2 direcao, float distancia, int mascaraCamadas, Color cor)
        {
            Debug.DrawLine(origen, origen + (direcao.normalized * distancia), cor);
            return Physics2D.Raycast(origen, direcao, distancia, mascaraCamadas);
        }
    }
}