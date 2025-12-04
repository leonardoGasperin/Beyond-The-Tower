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
        private float direcao;
        private float tempo = 0f;
        private float tempoTroca = 1.5f;
        private Vector2 direcaoOlha = Vector2.left;
        private bool viuJogador;
        private float duracaoAtaque = 0.3f;
        private float tempoAtacando = 0f;
 
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
            velocidade = 2f;
            viuJogador = false;
        }

        protected override void Update(){
            base.Update();

            hpVerde.fillAmount = (float)pontosVida/maxHP;

            if(estaVivo && pontosVida <= 0) {
                GetComponent<BoxCollider2D>().enabled = false;
                jogador.pontosEnergia += 2;
                estaVivo = false;
                Destroy(gameObject, 1.5f);
            }

            if(estaVivo == false) return;

            tempo += Time.deltaTime;
            if(tempo >= tempoTroca) {
                TrocaDirecao();
                tempo = 0;
            }

            distanciaDoJogador = jogador.transform.position.x - transform.position.x;
            direcao = Mathf.Sign(distanciaDoJogador);

            RaycastHit2D detectaChao = Physics2D.Raycast(transform.position  + new Vector3(0, -1f, 0), Vector2.down, 0.5f);
            Debug.DrawRay(transform.position  + new Vector3(0, -0.3f, 0), Vector2.down, Color.yellow);

            RaycastHit2D verJogador = Physics2D.Raycast(transform.position  + new Vector3(OrigemRay(), 0, 0), direcaoOlha, 10f);
            Debug.DrawRay(transform.position + new Vector3(OrigemRay(), 0, 0), direcaoOlha * 10f, Color.yellow);

            if(detectaChao.collider == null) viuJogador = false;

            if(detectaChao.collider == null && direcao < 0) {
                Vector3 posicao = transform.position;
                posicao.x += 0.01f;
                transform.position = posicao;
            }

            if(detectaChao.collider == null && direcao > 0) {
                Vector3 posicao2 = transform.position;
                posicao2.x -= 0.01f;
                transform.position = posicao2;
            }

            if(verJogador.collider != null && verJogador.collider.CompareTag("Jogador") && detectaChao.collider != null && Mathf.Abs(distanciaDoJogador) > 0) {
                viuJogador = true;
            }

            if(viuJogador) {
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

        private Vector2 TrocaDirecao(){
            if(direcaoOlha == Vector2.left) direcaoOlha = Vector2.right;
            else {
                direcaoOlha = Vector2.left;
            }
            return direcaoOlha;
        }

        private int OrigemRay(){
            if(direcaoOlha == Vector2.left) {
                return -1;
            } else {
                return 1;
            }
        }

        private void IntervaloAtaqueInimigo(){
            timerCooldown -= Time.deltaTime;
            if (timerCooldown <= 0) {
                timerCooldown = ataqueCooldown;
                estaAtacando = true;
                tempoAtacando = duracaoAtaque;
                fachada.combateServico.Atacando(ataque, alvo, pontosEnergia);
            }

            if (estaAtacando) {
                tempoAtacando -= Time.deltaTime;
                if (tempoAtacando <= 0)
                    estaAtacando = false;
            }
        }

        protected override void OnCollisionEnter2D(Collision2D col){
            base.OnCollisionEnter2D(col);
            velocidade = 0;
        }

        protected override void OnCollisionExit2D(Collision2D col){
            base.OnCollisionExit2D(col);
            velocidade = 2f;
        }
        
    }
}
