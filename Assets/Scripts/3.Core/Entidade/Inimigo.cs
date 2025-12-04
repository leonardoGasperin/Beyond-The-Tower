using UnityEngine;

namespace btt.Core.Entidade
{
    // TODO: Adequar para que a Entidade Inimigo seja genérica e reutilizável para todos os tipos de inimigos
    public class Inimigo : Personagem
    {
        public GameObject projetil;

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
        public bool podeAtirar;

        protected override void Start()
        {
            base.Start();
            tagAlvo = "Jogador";
            jogador = GameObject.FindGameObjectWithTag("Jogador").GetComponent<Jogador>();
        }

        protected override void Update()
        {
            base.Update();
            if (estaVivo == false) return;
            if (invencivel)
                return;

            if (!estaVivo)
            {
                GetComponent<BoxCollider2D>().enabled = false;
                jogador.pontosEnergia += 2;
            }

            /// TODO: mudar para ser organico
            tempo += Time.deltaTime;

            if (podeAtacar && alvo != null && alvo.estaVivo) IntervaloAtaqueInimigo();

        }

        /// TODO: remover metodo para trocar direcao ser mais organico
        public virtual void TrocaDirecao()
        {
            if (tempo < tempoTroca) return;

            if(direcaoOlha == Vector2.left)
                direcaoOlha = Vector2.right;
            else
                direcaoOlha = Vector2.left;

            tempo = 0;
        }

        // TODO: Refatorar
        protected int OrigemRay(){
            if(direcaoOlha == Vector2.left) {
                return -1;
            } else {
                return 1;
            }
        }

        /// TODO: Refatorar
        private void IntervaloAtaqueInimigo(){
            timerCooldown -= Time.deltaTime;
            if (timerCooldown <= 0) {
                timerCooldown = ataqueCooldown;
                estaAtacando = true;
                tempoAtacando = duracaoAtaque;
                fachada.combateServico.Atacando(ataque, alvo, 3 /*pontosEnergia*/);
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

        protected virtual void DetectarJogador(RaycastHit2D detectador)
        {
            distanciaDoJogador = jogador.transform.position.x - transform.position.x;
            if (detectador.collider != null && detectador.collider.CompareTag("Jogador") && Mathf.Abs(distanciaDoJogador) > 0)
                viuJogador = true;

            direcao = Mathf.Sign(distanciaDoJogador);
            if (viuJogador)
                fachada.movimentacaoServico.Movimentacao(transform, velocidade, direcao);

            /// TODO: mudar para ser organico
            if (distanciaDoJogador > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                barraHP.transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
                barraHP.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }

        protected virtual void DetectarChao(RaycastHit2D detectador)
        {
            if (detectador.collider == null) viuJogador = false;

            if (detectador.collider == null && direcao < 0)
            {
                Vector2 posicao = transform.position;
                posicao.x += 0.01f;
                transform.position = posicao;
            }

            if (detectador.collider == null && direcao > 0)
            {
                Vector2 posicao = transform.position;
                posicao.x -= 0.01f;
                transform.position = posicao;
            }
        }
        
    }
}
