using UnityEngine;
using UnityEngine.InputSystem;
using btt.Aplicacao.DI.Personagem;
using UnityEngine.UI;

namespace btt.Core.Entidade {

    public class Jogador : Personagem {

        private JogadorController jogadorController;
        private Inimigo alvo;
        private GerenciadorUI ui;
        [SerializeField] private GameObject barraHP;
        public Image hpVerde;

        protected override void Start(){
            base.Start();
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
            
            if (podeAtacar && alvo != null) JogadorPodeAtacar();

            if (jogadorController.BotaoPulo() && pontosEnergia > 0 && pontosVida > 0){
                fachada.movimentacaoServico.Pulo(rb, transform, forcaDoPulo);
                pontosEnergia = fachada.energiaServico.ReduzirEnergia(pontosEnergia);
            }

            if (pontosVida <= 0) ui.GameOver();

            hpVerde.fillAmount = (float)pontosVida/maxHP;
        }

        private void JogadorPodeAtacar(){
            if (alvo.pontosVida <= 0) {
                podeAtacar = false;
                alvo = null;
            }
            else if(podeAtacar && jogadorController.BotaoAtaque()) {
                JogadorAtaca();
            }
        }

        private void JogadorAtaca(){
            var estadoInimigo = fachada.combateServico.Atacando(ataque, alvo, pontosEnergia);
                if (!estadoInimigo) {
                    pontosEnergia = pontosEnergia + 2;
                    pontosVida ++;
                }
        }
        
        protected override void OnCollisionEnter2D(Collision2D col){
            base.OnCollisionEnter2D(col);
            if(col.gameObject.CompareTag("Inimigo")){
                podeAtacar = true;
                alvo = col.gameObject.GetComponent<Inimigo>();
            }
        }

        protected override void OnCollisionExit2D(Collision2D col){
            base.OnCollisionExit2D(col);
            if(col.gameObject.CompareTag("Inimigo")){
                podeAtacar = false;
                alvo = null;
            }
        }
    }
}