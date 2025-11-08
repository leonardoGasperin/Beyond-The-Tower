using UnityEngine;
using btt.Aplicacao.DI.Personagem;
using DialogueSystem;
using UnityEngine.UI;

namespace btt.Core.Entidade {
    public class MiniBoss : Personagem
    {
        private float ataqueCooldown = 2f;
        private float timerCooldown = 0f;
        private Transform barraHP;
        public Image hpVerde;
        public GameObject projetil;
        private Jogador jogador;
        private Vector3 offset;
        private float distancia;
        private bool atirouLanca;
        private float diferencaLanca;
        private float direcaoLanca;
        private Projetil lancaAtual;
        private bool podeAtirar = true;
        public float velocidadeSaidaColisao = 3f;
        private bool colidindo = false;
        
        protected override void Start(){
            base.Start();
            jogador = GameObject.FindGameObjectWithTag("Jogador").GetComponent<Jogador>();
            tagAlvo = "Jogador";
            velocidade = 0;
            maxHP = 100;
            ataque = 2;
            pontosVida = 100;
            atirouLanca = false;
            barraHP = transform.Find("Barra de HP");
            hpVerde = barraHP.transform.Find("HP Base/HP").GetComponent<Image>();
        }

        protected override void Update(){
            base.Update();

            hpVerde.fillAmount = (float)pontosVida/maxHP;

            if (pontosVida <= 0 || jogador.pontosVida <= 0) {
                return;
            }

            if(podeAtacar && alvo != null && alvo.pontosVida > 0) IntervaloAtaqueInimigo();

            if(colidindo) return;

            if(atirouLanca == false && podeAtirar){
                podeAtirar = false;
                lancaAtual = fachada.lancaServico.AtiraLanca(jogador, projetil, transform);
                velocidade = 0;
                atirouLanca = true;
            }

            if(atirouLanca && lancaAtual != null && lancaAtual.chao) {
                velocidade = 3f;
                diferencaLanca = transform.position.x - lancaAtual.transform.position.x;
                direcaoLanca = Mathf.Sign(diferencaLanca);
                fachada.movimentacaoServico.Movimentacao(transform, velocidade, -direcaoLanca);
            }
        }

        private void OnTriggerEnter2D(Collider2D col){
            if(col.gameObject.CompareTag("Projetil") && lancaAtual.chao){
                Destroy(col.gameObject);
                velocidade = 0;
                atirouLanca = false;
                podeAtirar = true;
            }
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
            colidindo = true;
            velocidade = 0;
        }

        protected override void OnCollisionExit2D(Collision2D col){
            base.OnCollisionExit2D(col);
            colidindo = false;
            velocidade = velocidadeSaidaColisao;
        }
    }
}
