using UnityEngine.InputSystem;
using btt.Aplicacao.DI.Personagem;

namespace btt.Core.Entidade {

    public class Jogador : Personagem {

        private JogadorController jogadorController;

        protected override void Start(){
            base.Start();
            jogadorController = new JogadorController();
        }

        protected override void Update(){
            base.Update();
            if(Keyboard.current == null) return;

            int direcional = jogadorController.BotoesDirecao();

            if (jogadorController.BotaoPulo(estaChao))
                fachada.movimentacaoServico.Pulo(rb, posicao, forcaDoPulo);
            if (direcional != 0)
                fachada.movimentacaoServico.Movimentacao(rb, velocidade, direcional);
        }
    }
}