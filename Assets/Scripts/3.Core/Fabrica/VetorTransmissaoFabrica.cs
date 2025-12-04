using UnityEngine;

namespace btt.Core.Fabrica
{
    public static class VetorTransmissaoFabrica
    {
        public static RaycastHit2D CriarVetorTransmissaoServico(Vector2 origen, Vector2 direcao, float distancia)
            => Physics2D.Raycast(origen, direcao, distancia);

        public static RaycastHit2D CriarVetorTransmissaoServicoDebug(Vector2 origen, Vector2 direcao, float distancia, Color cor)
        { 
            Debug.DrawRay(origen, direcao, cor);
            return Physics2D.Raycast(origen, direcao, distancia);
        }
    }
}