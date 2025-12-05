using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace btt.Core.Entidade
{
    // TODO: Adequar para que a Entidade Inimigo seja genérica e reutilizável para todos os tipos de inimigos
    public class Inimigo : Personagem
    {
        public GameObject projetil;
        public bool podeAtirar;

        protected Jogador jogador;
        protected Vector2 direcaoOlha = Vector2.left;
        protected float ataqueCooldown = 2f;
        protected float timerCooldown = 0f;
        protected float distancia;
        protected float distanciaDoJogador;
        protected float direcao;
        protected float tempo = 0f;
        protected float tempoTroca = 1.5f;
        protected float duracaoAtaque = 0.3f;
        protected float tempoAtacando = 0f;
        protected bool viuJogador;
        protected bool podeAndar;
        protected bool podeAtacarDistancia;
        protected bool podeVoar;

        protected override void Start()
        {
            base.Start();
            tagAlvo = "Jogador";
            jogador = GameObject.FindGameObjectWithTag("Jogador").GetComponent<Jogador>();
        }

        protected override void Update()
        {
            base.Update();
            if (invencivel) return;

            if (!estaVivo)
            {
                GetComponent<BoxCollider2D>().enabled = false;
                jogador.pontosEnergia += 2;
            }

            tempo += Time.deltaTime;
            timerCooldown -= Time.deltaTime;
            tempoAtacando -= Time.deltaTime;

            if (podeAtacar && alvo != null && alvo.estaVivo) IntervaloAtaqueInimigo();

        }

        public virtual void TrocaDirecao()
        {
            if (viuJogador) return;
            if (tempo <= tempoTroca) return;
            direcaoOlha.x *= -1;
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y == 0 ? 180 : 0, 0);
            tempo = 0;
        }

        private void IntervaloAtaqueInimigo(){
            if (timerCooldown <= 0) {
                timerCooldown = ataqueCooldown;
                estaAtacando = true;
                tempoAtacando = duracaoAtaque;
                fachada.combateServico.Atacando(ataque, alvo);
            }

            if (estaAtacando && tempoAtacando <= 0)
                estaAtacando = false;
        }

        protected override void OnCollisionEnter2D(Collision2D col){
            base.OnCollisionEnter2D(col);
            velocidade = 0;
        }

        protected override void OnCollisionExit2D(Collision2D col){
            base.OnCollisionExit2D(col);
            velocidade = 2f;
        }

        protected virtual void DetectarJogador(RaycastHit2D detectador)
        {
            distanciaDoJogador = jogador.transform.position.x - transform.position.x;
            if (podeAndar && detectador.collider != null && detectador.collider.CompareTag("Jogador"))
                viuJogador = true;
            else
                viuJogador = false;

            if (!viuJogador) return;

            direcao = Mathf.Sign(distanciaDoJogador);
            if (viuJogador)
                fachada.movimentacaoServico.Movimentacao(transform, velocidade, direcao);

            barraHP.transform.localRotation = Quaternion.Euler(0, direcao <= 0 ? 180 : 0, 0);
        }

        protected virtual void DetectarChao(RaycastHit2D detectador)
        {
            if (detectador.collider == null)
            {
                viuJogador = false;
                podeAndar = false;
                direcao *= -1;
                return;
            }

            podeAndar = true;
        }
        
    }
}
