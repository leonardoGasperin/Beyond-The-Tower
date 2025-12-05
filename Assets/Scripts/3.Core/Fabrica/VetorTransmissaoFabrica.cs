using UnityEngine;

namespace btt.Core.Fabrica
{
    public static class VetorTransmissaoFabrica
    {
        public static RaycastHit2D CriarVetorTransmissaoServico(Vector2 origen, Vector2 direcao, float distancia)
            => Physics2D.Raycast(origen, direcao, distancia);

        public static RaycastHit2D CriarVetorTransmissaoServicoDebug(Vector2 origen, Vector2 direcao, float distancia, Color cor)
        {
            Debug.DrawLine(origen, origen + (direcao.normalized * distancia), cor);
            return Physics2D.Raycast(origen, direcao, distancia);
        }
    }
}