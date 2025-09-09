using UnityEngine;
using btt.Aplicacao.DI.Personagem;

namespace btt.Core.Entidade
{
    /// <summary>
    /// Classe base para personagens no jogo, contendo atributos e m�todos comuns.
    /// </summary>
    /// <remarks>
    /// Esta classe define os atributos b�sicos de um personagem, como pontos de vida, ataque, defesa, etc.
    /// Tamb�m implementa a l�gica de dano e morte do personagem.
    /// </remarks>
    public class Personagem : MonoBehaviour
    {
        #region Atributos
        public PersonagemConfiguracaoDI.ServiceLocator fachada;
        public Rigidbody2D rb;
        public string nome;
        public int pontosVida;
        public int pontosEnergia;
        public int ataque;
        public int defesa;
        public int nivel;
        public int experiencia;
        public float forcaDoPulo;
        public Transform posicao;
        //Animator animacao;
        public float velocidade;
        public bool estaVivo;
        public bool estaAtacando;
        public bool estaDefendendo;
        public bool estaChao;
        public bool ativo = true;
        #endregion

        #region Unity Methods

        protected virtual void Awake(){
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        protected virtual void Start()
        {
            fachada = GetComponent<PersonagemConfiguracaoDI>().Services;
            rb = GetComponent<Rigidbody2D>();
            posicao = GetComponent<Transform>();
            //animacao = GetComponent<Animator>();
            estaVivo = true;
            estaAtacando = false;
            estaDefendendo = false;
        }

        // Update is called once per frame
        protected virtual void Update()
        {

        }

    //Checar se o jogador está tocando o chão
        void OnCollisionEnter2D(Collision2D col){
            if(col.gameObject.CompareTag("Plataforma")) {
                estaChao = true;
            }
    }

    //Checar se o jogador não está tocando o chão
        void OnCollisionExit2D(Collision2D col){
            if(col.gameObject.CompareTag("Plataforma")){
                estaChao = false;
            }
    }

        #endregion

        #region Methods
        protected virtual void Dano(int danoRecebido)
        {
            int danoFinal = danoRecebido - defesa;
            if (danoFinal < 0)
            {
                danoFinal = 0;
            }
            pontosVida -= danoFinal;
            if (pontosVida <= 0)
            {
                pontosVida = 0;
                Morreu();
            }
        }

        private void Morreu()
        {
            estaVivo = false;
            ativo = false;
            //animacao.SetTrigger("Morreu");
            // Desativar o personagem ou iniciar a l�gica de rein�cio
        }

        public void Atacar(Personagem alvo)
        {
            if (estaVivo && !estaAtacando)
            {
                estaAtacando = true;
                //animacao.SetTrigger("Ataca");
                alvo.Dano(ataque);
                estaAtacando = false;
            }
        }

        #endregion
    }
}