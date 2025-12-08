using btt.Aplicacao.DI.Personagem;
using UnityEngine;
using UnityEngine.UI;

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
        /// TODO: rever props, há sinais de overengineering
        protected PersonagemConfiguracaoDI.ServiceLocator fachada;
        //Animator animacao;
        public Transform posicao;
        public Rigidbody2D rb;
        public Transform barraHP;
        public Image hpVerde;
        public Personagem alvo;
        public string tagAlvo;
        public string nome;
        public bool ativo = true;
        public bool estaVivo;
        public bool podeAtacar;
        public bool estaDefendendo;
        public bool estaChao;
        public bool invencivel;
        public float velocidade;
        public float forcaDoPulo;
        public int ataque;
        public int defesa;
        public int maxHP;
        public int pontosVida;
        #endregion

        #region Unity Methods
        protected virtual void Awake() { }


        protected virtual void Start()
        {
            fachada = GetComponent<PersonagemConfiguracaoDI>().Services;
            rb = GetComponent<Rigidbody2D>();
            posicao = GetComponent<Transform>();
            //animacao = GetComponent<Animator>();
            estaVivo = true;
            estaDefendendo = false;

            if (!invencivel)
            {
                barraHP = transform.Find("Barra de HP");
                hpVerde = barraHP.transform.Find("HP Base/HP").GetComponent<Image>();
            }
        }


        protected virtual void Update()
        {
            if (!invencivel)
                hpVerde.fillAmount = (float)pontosVida / maxHP;
        }


        protected virtual void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag(tagAlvo))
            {
                podeAtacar = true;
                alvo = col.gameObject.GetComponent<Personagem>();
            }
        }


        protected virtual void OnCollisionExit2D(Collision2D col)
        {
            if (col.gameObject.CompareTag(tagAlvo))
            {
                podeAtacar = false;
                alvo = null;
            }
        }

        #endregion

        #region Methods
        public virtual void Dano(int danoRecebido)
        {
            int danoFinal = danoRecebido - defesa;
            if (danoFinal <= 0) return;

            pontosVida -= danoFinal;
            if (pontosVida > 0) return;

            pontosVida = 0;
            estaVivo = false;
        }

        public virtual void Morreu()
        {
            ativo = false;
            podeAtacar = false;
            estaDefendendo = false;
            estaChao = true;

            //animacao.SetTrigger("Morreu");
            // Desativar o personagem ou iniciar a l�gica de rein�cio
        }
        #endregion
    }
}