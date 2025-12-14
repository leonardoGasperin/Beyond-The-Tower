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
        }

        protected override void Update()
        {
            base.Update();
            Comportamento();
        }

        private void Comportamento()
        {
            var visaoOrientacao = transform.position + new Vector3(direcaoOlha.x, 0, 0);
            var chaoOrientacaoRelacao = transform.position + new Vector3(0.5f * direcaoOlha.x, -1f, 0);

#if DEBUG
            var detectorJogador = VetorTransmissaoFabrica.CriarVetorTransmissaoServicoDebug(visaoOrientacao, direcaoOlha, 4f, mascaraCamada, Color.red);
            var detectorChao = VetorTransmissaoFabrica.CriarVetorTransmissaoServicoDebug(chaoOrientacaoRelacao, Vector2.down, 0.5f, mascaraCamada, Color.yellow);
#endif
#if !DEBUG
            var detectorJogador = VetorTransmissaoFabrica.CriarVetorTransmissaoServico(visaoOrientacao, direcaoOlha, 4f, mascaraCamada);
            var detectorChao = VetorTransmissaoFabrica.CriarVetorTransmissaoServico(chaoOrientacaoRelacao, Vector2.down, 0.5f, mascaraCamada);
#endif

            DetectarJogador(detectorJogador);
            DetectarChao(detectorChao);
            TrocaDirecao();
        }

    }
}