using btt.Core.Fabrica;
using UnityEngine;

namespace btt.Core.Entidade
{
    public class Ratinho : Inimigo
    {
        #region Unit Metodos
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag == "Jogador")
                fachada.combateServico.Atacando(ataque, jogador);
        }

        #endregion

        #region Herdados Metodos
        protected override void Start()
        {
            base.Start();
            pontosVida = 2;
            velocidade = 5;
            ataque = 1;
            direcao = 1;
        }

        protected override void Update()
        {
            base.Update();
            Comportamento();
        }

        public virtual void TrocaDirecao(Collider2D col)
            => transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y == 0 ? 180 : 0, 0);
        #endregion

        #region Entidade Metodos
        private void Comportamento()
        {
            var origem = transform.position + new Vector3(0.5f * direcao, 0, 0);
            var distanciaChao = 1f;
            var detectorChao = VetorTransmissaoFabrica.CriarVetorTransmissaoServicoDebug(origem, Vector2.up, distanciaChao, mascaraCamada, Color.yellow);

            if (detectorChao.collider == null || (detectorChao.collider != null && detectorChao.collider.CompareTag("Parede")))
                TrocaDirecao(detectorChao.collider);
            DetectarChao(detectorChao);
            fachada.movimentacaoServico.Movimentacao(transform, velocidade, direcao);
        }
        #endregion

    }
}