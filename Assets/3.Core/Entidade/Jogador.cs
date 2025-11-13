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
        private GerenciadorCamera mainCamera;
        public int direcional;
        public bool podePular;
        public bool anda;

        protected override void Start(){
            base.Start();
            tagAlvo = "Inimigo";
            jogadorController = new JogadorController();
            pontosEnergia = 10000;
            velocidade = 5f;
            pontosVida = 10;
            maxHP = 10;
            ataque = 2;
            forcaDoPulo = 10;
            podeAtacar = false;
            anda = true;
            podePular = true;
            ui = GameObject.Find("GerenciadorUI").GetComponent<GerenciadorUI>();
            hpVerde = barraHP.transform.Find("HP Base/HP").GetComponent<Image>();
        }

        protected override void Update(){
            base.Update();

            if(Keyboard.current == null) return;

            if(pontosVida > maxHP) pontosVida = maxHP;

            direcional = jogadorController.BotoesDirecao();

            if (direcional != 0 && pontosVida > 0 && anda)
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

            if (jogadorController.BotaoPulo() && podePular && pontosVida > 0){
                fachada.movimentacaoServico.Pulo(rb, transform, forcaDoPulo);
                pontosEnergia = fachada.energiaServico.ReduzirEnergia(pontosEnergia);
            }

            if(pontosEnergia <= 0) podePular = false;

            if (pontosVida <= 0) ui.GameOver();

            hpVerde.fillAmount = (float)pontosVida/maxHP;
        }

        private void OnTriggerStay2D(Collider2D col){
            if (col.CompareTag(tagAlvo)) {
                podeAtacar = true;
                alvo = col.GetComponent<Personagem>();
            }
        }
    }
}