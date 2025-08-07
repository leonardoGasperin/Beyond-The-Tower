using UnityEngine;
using static PersonagemFachadaServico;

namespace btt.Core.Entidade
{
    /// <summary>
    /// Classe base para personagens no jogo, contendo atributos e métodos comuns.
    /// </summary>
    /// <remarks>
    /// Esta classe define os atributos básicos de um personagem, como pontos de vida, ataque, defesa, etc.
    /// Também implementa a lógica de dano e morte do personagem.
    /// </remarks>
    public class Personagem : MonoBehaviour
    {
        /// <summary>
        /// Propriedade que fornece acesso aos serviços resolvidos para este escopo.
        /// </summary>
        public ServiceLocator Services { get; private set; }

        #region Atributos
        public string nome;
        public int pontosVida;
        public int pontosEnergia;
        public int ataque;
        public int defesa;
        public int nivel;
        public int experiencia;
        Transform posicao;
        //Animator animacao;
        public int velocidade;
        public int forcaPulo;
        public bool estaVivo;
        public bool estaAtacando;
        public bool estaDefendendo;
        public bool estaChao;
        public bool ativo = true;
        #endregion

        #region Unity Methods

        /// <summary>
        /// Inicializa o Service Locator ao acordar a entidade, obtendo-o do componente associado.
        /// </summary>
        public virtual void Awake()
        {
            Services = GetComponent<PersonagemFachadaServico>().Services;
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            posicao = GetComponent<Transform>();
            //animacao = GetComponent<Animator>();
            estaVivo = true;
            estaAtacando = false;
            estaDefendendo = false;
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
            // Desativar o personagem ou iniciar a lógica de reinício
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