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
        }

        protected override void Update(){
            base.Update();
            if(Keyboard.current == null) return;

            int direcional = jogadorController.BotoesDirecao();

            if (jogadorController.BotaoPulo(estaChao)){
                fachada.movimentacaoServico.Pulo(rb, posicao, forcaDoPulo);
                fachada.energiaServico.ReduzirEnergia(pontosEnergia);
                Debug.Log(pontosEnergia);
            }

            if (direcional != 0)
                fachada.movimentacaoServico.Movimentacao(transform, velocidade, direcional);

            void OnCollisionEnter2D(Collision2D col) {
                if(jogadorController.BotaoAtaque()) fachada.combateServico.Atacando(ataque, col, pontosEnergia);
           }
        }
    }
}