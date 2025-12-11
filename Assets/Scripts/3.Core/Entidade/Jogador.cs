using btt.Configuracao.DI.Jogador;
using UnityEngine;
using UnityEngine.InputSystem;

namespace btt.Core.Entidade
{
    public sealed class Jogador : Personagem
    {
        #region Atributos
        private JogadorConfiguracaoDI.ServiceLocator JogadorFachada;
        private JogadorController jogadorController;
        private GerenciadorUI ui;
        public int pontosEnergia;
        public int direcional;
        public int nivel;
        public int experiencia;
        public bool emDialogo;

        #endregion

        #region Herdados Metodos
        protected override void Start()
        {
            base.Start();
            JogadorFachada = GetComponent<JogadorConfiguracaoDI>().Services;
            tagAlvo = "Inimigo";
            jogadorController = new JogadorController();
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

            direcional = jogadorController.BotoesDirecao();
            Movimento();
            Ataque();
            Pulo();
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
            if (direcional == 0) return;

            Orientacao();
            fachada.movimentacaoServico.Movimentacao(transform, velocidade, direcional);
        }

        private void Pulo()
        {
            if (!EnergiaPuloControle() || !jogadorController.BotaoPulo()) return;

            fachada.movimentacaoServico.Pulo(rb, transform, forcaDoPulo);
            pontosEnergia = JogadorFachada.energiaServico.ReduzirEnergia(pontosEnergia);
        }

        private bool EnergiaPuloControle()
        {
            if (pontosEnergia <= 0)
                return false;
            else
                return true;
        }

        private void Ataque()
        {
            if (alvo != null && !alvo.estaVivo)
            {
                podeAtacar = false;
                alvo = null;
                return;
            }

            if (podeAtacar && alvo != null && jogadorController.BotaoAtaque())
                fachada.combateServico.Atacando(ataque, alvo);
        }

        #endregion
    }
}