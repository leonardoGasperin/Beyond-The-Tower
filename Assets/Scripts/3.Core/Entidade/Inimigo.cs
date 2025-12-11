using btt.Configuracao.DI.Inimigo;
using btt.Apresentacao.Gerenciadores.InimigoGerenciador;
using UnityEngine;

namespace btt.Core.Entidade
{
    public class Inimigo : Personagem
    {
        #region Atributos
        protected InimigoConfiguracaoDI.ServiceLocator InimigoFachada;
        public GameObject projetil;
        public bool podeAtirar;
        public bool estaAtacando;

        protected Jogador jogador;
        protected Vector2 direcaoOlha = Vector2.left;
        protected bool podeAndar;
        protected bool podeAtacarDistancia;
        protected bool podeVoar;
        protected bool viuJogador;
        [SerializeField]
        protected int energiaRecompensa;
        protected float ataqueCooldown = 2f;
        protected float timerCooldown = 0f;
        protected float distancia;
        protected float distanciaDoJogador;
        protected float direcao;
        protected float tempo = 0f;
        protected float tempoTroca = 1.5f;
        protected float tempoAtacando = 0f;

        public int EnergiaRecompensa => energiaRecompensa;

        #endregion

        #region Herdados Metodos
        protected override void Start()
        {
            base.Start();
            estaAtacando = false;
            tagAlvo = "Jogador";
            jogador = GameObject.FindGameObjectWithTag(tagAlvo).GetComponent<Jogador>();
        }

        protected override void Update()
        {
            base.Update();
            if (jogador == null) return;

            tempo += Time.deltaTime;
            timerCooldown -= Time.deltaTime;

            if (podeAtacar && alvo != null && alvo.estaVivo) IntervaloAtaqueInimigo();
            if (!estaVivo) Morreu();
        }
        protected override void OnCollisionEnter2D(Collision2D col)
        {
            base.OnCollisionEnter2D(col);
            velocidade = 0;
        }

        protected override void OnCollisionExit2D(Collision2D col)
        {
            base.OnCollisionExit2D(col);
            velocidade = 2f;
        }

        protected override void Morreu()
        {
            base.Morreu();

            estaAtacando = false;
            GetComponent<BoxCollider2D>().enabled = false;
            jogador.pontosEnergia += EnergiaRecompensa;
            GerenciadorInimigo.Instance.DestruirInimigo(this);
        }

        #endregion

        #region Entidade Metodos
        protected virtual void ObjetoOrientacao()
        {
            if (distancia > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
                barraHP.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
                barraHP.transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }

        protected virtual void TrocaDirecao()
        {
            if (viuJogador || tempo <= tempoTroca) return;
            direcaoOlha.x *= -1;
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y == 0 ? 180 : 0, 0);
            tempo = 0;
        }

        protected virtual void DetectarJogador(RaycastHit2D detectador)
        {
            viuJogador = podeAndar && detectador.collider != null && detectador.collider.CompareTag("Jogador");
            if (!viuJogador) return;

            float distanciaDoJogador = jogador.transform.position.x - transform.position.x;
            direcao = Mathf.Sign(distanciaDoJogador);
            fachada.movimentacaoServico.Movimentacao(transform, velocidade, direcao);
            barraHP.transform.localRotation = Quaternion.Euler(0, direcao <= 0 ? 180 : 0, 0);
        }

        protected virtual void DetectarChao(RaycastHit2D detectador)
        {
            if (detectador.collider != null && !detectador.collider.CompareTag("Parede"))
            {
                podeAndar = true;
                return;
            }

            viuJogador = false;
            podeAndar = false;
            direcao *= -1;
            return;
        }

        protected virtual void IntervaloAtaqueInimigo()
        {
            if (timerCooldown > 0) return;

            timerCooldown = ataqueCooldown;
            estaAtacando = true;
            fachada.combateServico.Atacando(ataque, alvo);
            estaAtacando = false;
        }

        #endregion

    }
}
