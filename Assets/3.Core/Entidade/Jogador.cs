using UnityEngine;
using UnityEngine.InputSystem;
using btt.Aplicacao.DI.Personagem;

namespace btt.Core.Entidade {

    public class Jogador : Personagem {

        private JogadorController jogadorController;
        private Inimigo alvo;

        protected override void Start(){
            base.Start();
            jogadorController = new JogadorController();
            pontosEnergia = 3;
            pontosVida = 10;
            ataque = 2;
            forcaDoPulo = 5;
            podeAtacar = false;
        }

        protected override void Update(){
            base.Update();

            if(Keyboard.current == null) return;

            int direcional = jogadorController.BotoesDirecao();

            if (direcional != 0 && pontosVida > 0)
                fachada.movimentacaoServico.Movimentacao(transform, velocidade, direcional);
            
            if (podeAtacar && alvo != null) {
                if (alvo.pontosVida <= 0) {
                    podeAtacar = false;
                    alvo = null;
                }
                else if(podeAtacar && jogadorController.BotaoAtaque()) {
                    var estadoInimigo = fachada.combateServico.Atacando(ataque, alvo, pontosEnergia);
                    Debug.Log("Inimigo pontos de vida: " + alvo.pontosVida);
                    if (!estadoInimigo) pontosEnergia++;
                    Debug.Log("Jogador pontos de energia: " + pontosEnergia);
                }
            }

            if (jogadorController.BotaoPulo(estaChao) && pontosEnergia > 0 && pontosVida > 0){
                fachada.movimentacaoServico.Pulo(rb, transform, forcaDoPulo);
                pontosEnergia = fachada.energiaServico.ReduzirEnergia(pontosEnergia);
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