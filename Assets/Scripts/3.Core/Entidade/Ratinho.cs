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
            var origem = transform.position + new Vector3(ray * direcao, 0, 0);
            var distanciaChao = 1f;
            var detectorChao = VetorTransmissaoFabrica.CriarVetorTransmissaoServicoDebug(origem, Vector2.up, distanciaChao, Color.yellow);

            if (detectorChao.collider == null || (detectorChao.collider != null && detectorChao.collider.CompareTag("Parede")))
                TrocaDirecao(detectorChao.collider);
            DetectarChao(detectorChao);

            fachada.movimentacaoServico.Movimentacao(transform, velocidade, direcao);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag == "Jogador")
                fachada.combateServico.Atacando(ataque, jogador);
        }

        public virtual void TrocaDirecao(Collider2D col)
            => transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y == 0 ? 180 : 0, 0);
    }
}