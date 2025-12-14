using btt.Configuracao.DI.Jogador;
using UnityEngine;
using UnityEngine.InputSystem;

namespace btt.Core.Entidade
{
    public sealed class Jogador : Personagem
    {
        #region Atributos
        private JogadorConfiguracaoDI.ServiceLocator JogadorFachada;
        private GerenciadorUI ui;

        public Rigidbody2D rb;
        public InputActionReference movimentoInput;
        public InputActionReference puloInput;
        public InputActionReference ataqueInput;
        public int pontosEnergia;
        public float direcional;
        public int nivel;
        public int experiencia;
        public bool emDialogo;

        #endregion

        #region Unity Metodos
        private void OnEnable()
        {
            puloInput.action.started += Pulo;
            ataqueInput.action.started += Ataque;
        }

        private void OnDisable()
        {
            puloInput.action.started -= Pulo;
            ataqueInput.action.started -= Ataque;
        }

        #endregion

        #region Herdados Metodos
        protected override void Start()
        {
            base.Start();
            rb = GetComponent<Rigidbody2D>();
            JogadorFachada = GetComponent<JogadorConfiguracaoDI>().Services;
            tagAlvo = "Inimigo";
            pontosEnergia = 10000;
            velocidade = 5f;
            pontosVida = 500;
            maxHP = 1000;
            ataque = 20;
            forcaDoPulo = 10;
            ui = GameObject.Find("Gerenciador").GetComponent<GerenciadorUI>();
        }

        protected override void Update()
        {
            Morreu();
            base.Update();
            if (Keyboard.current == null || emDialogo)
                return;

            Movimento();
        }

        protected override void Morreu()
        {
            if (estaVivo) return;

            base.Morreu();
            ui.GameOver();
        }

        #endregion

        #region Entidade Metodos
        private void Orientacao()
        {
            if (direcional == 0) return;

            transform.localScale = new Vector3(direcional, 1, 1);
            barraHP.transform.localRotation = Quaternion.Euler(0, direcional == -1 ? 180 : 0, 0);
        }

        private void Movimento()
        {
            direcional = movimentoInput.action.ReadValue<float>();
            if (direcional == 0) return;

            Orientacao();
            fachada.movimentacaoServico.Movimentacao(transform, velocidade, direcional);
        }

        private void Pulo(InputAction.CallbackContext obj)
        {
            if (!EnergiaPuloControle()) return;

            fachada.movimentacaoServico.Pulo(rb, transform, forcaDoPulo);
            pontosEnergia = JogadorFachada.energiaServico.ReduzirEnergia(pontosEnergia);
        }

        private bool EnergiaPuloControle()
        {
            if (pontosEnergia <= 0)
                return false;
            
            return true;
        }

        private void Ataque(InputAction.CallbackContext obj)
        {
            if (podeAtacar && alvo != null)
                fachada.combateServico.Atacando(ataque, alvo);
        }

        #endregion
    }
}