using btt.Core.Fabrica;
using UnityEngine;

namespace btt.Core.Entidade
{

    public class Ratinho : Inimigo
    {
        private float ray;

        protected override void Start()
        {
            base.Start();
            pontosVida = 2;
            velocidade = 5;
            ataque = 1;
            direcao = 1;
            ray = 0.5f;
        }

        protected override void Update()
        {
            base.Update();
            fachada.movimentacaoServico.Movimentacao(transform, velocidade, direcao);

            var origem = transform.position + new Vector3(ray * direcao, 0, 0);
            var distanciaChao = 1f;
            var detectorChao = VetorTransmissaoFabrica.CriarVetorTransmissaoServicoDebug(origem, Vector2.up, distanciaChao, Color.orchid);
            DetectarChao(detectorChao);
            TrocaDirecao(detectorChao.collider);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag == "Jogador")
                fachada.combateServico.Atacando(ataque, jogador);
        }

        public virtual void TrocaDirecao(Collider2D col)
        {
            if (col != null) return;
            direcao *= -1;
            Vector3 inverteDirecao = transform.localScale;
            inverteDirecao.x = Mathf.Abs(inverteDirecao.x) * direcao;
            transform.localScale = inverteDirecao;
        }
    }
}