using UnityEngine;
using System.Threading.Tasks;
using btt.Aplicacao.DI.Personagem;
using UnityEngine.UI;

namespace btt.Core.Entidade {

    public class Inimigo : Personagem {

        private float ataqueCooldown = 2f;
        private float timerCooldown = 0f;
        private Transform barraHP;
        public Image hpVerde;
        public Jogador jogador;
        public float distanciaDoJogador;
        public float velocidadeSaidaColisao;
        private float direcao;
    
        protected override void Start(){
            base.Start();
            tagAlvo = "Jogador";
            maxHP = 10;
            pontosVida = 10;
            ataque = 2;
            podeAtacar = false;
            barraHP = transform.Find("Barra de HP");
            hpVerde = barraHP.transform.Find("HP Base/HP").GetComponent<Image>();
            jogador = GameObject.FindGameObjectWithTag("Jogador").GetComponent<Jogador>();
        }

        protected override void Update(){
            base.Update();

            hpVerde.fillAmount = (float)pontosVida/maxHP;

            if(estaVivo && pontosVida <= 0) {
                GetComponent<BoxCollider2D>().enabled = false;
                jogador.pontosVida ++;
                jogador.pontosEnergia += 2;
                estaVivo = false;
                Destroy(gameObject, 1.5f);
            }

            if(estaVivo == false) return;

            distanciaDoJogador = jogador.transform.position.x - transform.position.x;

            if (Mathf.Abs(distanciaDoJogador) > 0) {
                direcao = Mathf.Sign(distanciaDoJogador);
                fachada.movimentacaoServico.Movimentacao(transform, velocidade, direcao);
            }

            if(distanciaDoJogador > 0) {
                transform.localScale = new Vector3(-1, 1, 1);
                barraHP.transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            else {
                transform.localScale = new Vector3(1, 1, 1);
                barraHP.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

            if(podeAtacar && alvo != null && alvo.pontosVida > 0) IntervaloAtaqueInimigo();

        }

        private void IntervaloAtaqueInimigo(){
            timerCooldown -= Time.deltaTime;
            if (timerCooldown <= 0) {
                timerCooldown = ataqueCooldown;
                fachada.combateServico.Atacando(ataque, alvo, pontosEnergia);
            }
        }

        protected override void OnCollisionEnter2D(Collision2D col){
            base.OnCollisionEnter2D(col);
            velocidade = 0;
        }

        protected override void OnCollisionExit2D(Collision2D col){
            base.OnCollisionExit2D(col);
            velocidade = velocidadeSaidaColisao;
        }
        
    }
}