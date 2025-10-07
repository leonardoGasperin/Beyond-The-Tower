using UnityEngine;
using UnityEngine.InputSystem;
using btt.Aplicacao.DI.Personagem;
using UnityEngine.UI;

namespace btt.Core.Entidade {

    public class Jogador : Personagem {

        private JogadorController jogadorController;
        private GerenciadorUI ui;
        [SerializeField] private GameObject barraHP;
        public Image hpVerde;
        private GerenciadorCamera camera;

        protected override void Start(){
            base.Start();
            tagAlvo = "Inimigo";
            jogadorController = new JogadorController();
            pontosEnergia = 3;
            pontosVida = 10;
            maxHP = 10;
            ataque = 2;
            forcaDoPulo = 10;
            podeAtacar = false;
            ui = GameObject.Find("GerenciadorUI").GetComponent<GerenciadorUI>();
            hpVerde = barraHP.transform.Find("HP Base/HP").GetComponent<Image>();
        }

        protected override void Update(){
            base.Update();

            if(Keyboard.current == null) return;

            int direcional = jogadorController.BotoesDirecao();

            if (direcional != 0 && pontosVida > 0)
                fachada.movimentacaoServico.Movimentacao(transform, velocidade, direcional);
            
            if(direcional == -1) {
                transform.localScale = new Vector3(-1, 1, 1);
                barraHP.transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            else if(direcional == 1) {
                transform.localScale = new Vector3(1, 1, 1);
                barraHP.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            
            if (podeAtacar && alvo.pontosVida > 0 && jogadorController.BotaoAtaque()) {
                fachada.combateServico.Atacando(ataque, alvo, pontosEnergia);
            }

            if (alvo != null && alvo.pontosVida <= 0) {
                podeAtacar = false;
                alvo = null;
            }

            if (jogadorController.BotaoPulo() && pontosEnergia > 0 && pontosVida > 0){
                fachada.movimentacaoServico.Pulo(rb, transform, forcaDoPulo);
                pontosEnergia = fachada.energiaServico.ReduzirEnergia(pontosEnergia);
            }

            if (pontosVida <= 0) ui.GameOver();

            hpVerde.fillAmount = (float)pontosVida/maxHP;
        }
    }
}