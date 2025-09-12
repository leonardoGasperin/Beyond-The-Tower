using UnityEngine;
using UnityEngine.InputSystem;
using btt.Aplicacao.DI.Personagem;

namespace btt.Core.Entidade {

    public class Jogador : Personagem {

        private JogadorController jogadorController;

        protected override void Start(){
            base.Start();
            jogadorController = new JogadorController();
            pontosEnergia = 3;
            pontosVida = 10;
            ataque = 2;
            forcaDoPulo = 5;
        }

        protected override void Update(){
            base.Update();
            if(Keyboard.current == null) return;

            int direcional = jogadorController.BotoesDirecao();

            if (jogadorController.BotaoPulo(estaChao)){
                fachada.movimentacaoServico.Pulo(rb, transform, forcaDoPulo);
                pontosEnergia = fachada.energiaServico.ReduzirEnergia(pontosEnergia);
            }

            if (direcional != 0)
                fachada.movimentacaoServico.Movimentacao(transform, velocidade, direcional);
            
        }
        
        void OnCollisionStay2D(Collision2D col){
            if(jogadorController.BotaoAtaque() && col.gameObject.CompareTag("Inimigo")){
                fachada.combateServico.Atacando(ataque, col, pontosEnergia);
            }
        }
    }
}