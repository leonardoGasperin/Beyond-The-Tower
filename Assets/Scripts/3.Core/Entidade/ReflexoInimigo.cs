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
            var visaoOrientacao = transform.position + new Vector3(direcaoOlha.x, 0, 0);
            var detectorJogador = VetorTransmissaoFabrica.CriarVetorTransmissaoServicoDebug(visaoOrientacao, direcaoOlha, 4f, Color.red);
            var chaoOrientacaoRelacao = transform.position + new Vector3(0.5f * direcaoOlha.x, -1f, 0);
            var detectorChao = VetorTransmissaoFabrica.CriarVetorTransmissaoServicoDebug(chaoOrientacaoRelacao, Vector2.down, 0.5f, Color.yellow);

            DetectarJogador(detectorJogador);
            DetectarChao(detectorChao);
            TrocaDirecao();
        }
    }
}