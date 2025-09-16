using UnityEngine;
using System.Threading.Tasks;
using btt.Aplicacao.DI.Personagem;

namespace btt.Core.Entidade {

    public class Inimigo : Personagem {

        private Jogador alvo;
        private float ataqueCooldown = 2f;
        private float timerCooldown = 0f;
    
        protected override void Start(){
            base.Start();
            pontosVida = 10;
            ataque = 2;
            podeAtacar = false;
        }

        protected override void Update(){
            base.Update();

            if(podeAtacar && alvo != null){
                timerCooldown -= Time.deltaTime;
                if (timerCooldown <= 0) {
                    timerCooldown = ataqueCooldown;
                    fachada.combateServico.Atacando(ataque, alvo, pontosEnergia);
                    Debug.Log("Jogador pontos de vida: " + alvo.pontosVida);
                }
            }
        }

        protected override void OnCollisionEnter2D(Collision2D col){
            base.OnCollisionEnter2D(col);
            if(col.gameObject.CompareTag("Jogador")){
                podeAtacar = true;
                alvo = col.gameObject.GetComponent<Jogador>();
            }
        }

        protected override void OnCollisionExit2D(Collision2D col){
            base.OnCollisionExit2D(col);
            if(col.gameObject.CompareTag("Jogador")){
                podeAtacar = false;
                alvo = null;
            }
        }
    }
}