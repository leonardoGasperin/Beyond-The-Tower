using UnityEngine;
using btt.Aplicacao.DI.Personagem;

namespace btt.Core.Entidade {

    public class Ratinho : Personagem
    {

        private int direcao;
        private Jogador jogador;
        private float ray;

        protected override void Start(){
            base.Start();
            jogador = GameObject.FindGameObjectWithTag("Jogador").GetComponent<Jogador>();
            pontosVida = 2;
            velocidade = 5;
            ataque = 1;
            direcao = 1;
            ray = 0.5f;
        }

        protected override void Update(){
            base.Update();

            fachada.movimentacaoServico.Movimentacao(transform, velocidade, direcao);
            RaycastHit2D teto = Physics2D.Raycast(transform.position + new Vector3(ray * direcao, 0, 0), Vector2.up, 1f);

            if(teto.collider == null) {
                direcao *= -1;
                Vector3 inverteDirecao = transform.localScale;
                inverteDirecao.x = Mathf.Abs(inverteDirecao.x) * direcao;
                transform.localScale = inverteDirecao;
            }

            if(pontosVida <= 0) Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D col){
            if (col.gameObject.tag == "Jogador") fachada.combateServico.Atacando(ataque, jogador, pontosEnergia);
        }
    }
}