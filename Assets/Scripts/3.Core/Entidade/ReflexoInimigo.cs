using btt.Core.Fabrica;
using UnityEngine;

namespace btt.Core.Entidade
{
    public class ReflexoInimigo : Inimigo
    {
        protected override void Start()
        {
            base.Start();
            maxHP = 10;
            pontosVida = 10;
            ataque = 2;
            podeAtacar = false;
            velocidade = 2f;
            viuJogador = false;
        }

        protected override void Update()
        {
            base.Update();
            var chaoOrientacaoRelacao = transform.position + new Vector3(0, -1f, 0);
            var chaoVetorDistancia = 0.5f;
            var visaoOrientacao = transform.position + new Vector3(direcaoOlha.x, 0, 0);
            var visaoDistancia = 10f;
            
            DetectarChao(VetorTransmissaoFabrica.CriarVetorTransmissaoServicoDebug(chaoOrientacaoRelacao, Vector2.down, chaoVetorDistancia, Color.yellow));
            DetectarJogador(VetorTransmissaoFabrica.CriarVetorTransmissaoServicoDebug(visaoOrientacao, direcaoOlha * 10f, visaoDistancia, Color.red));
            TrocaDirecao();
        }
    }
}