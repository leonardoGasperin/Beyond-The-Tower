using UnityEngine;
using System.Threading.Tasks;
using btt.Aplicacao.DI.Personagem;
using UnityEngine.UI;

namespace btt.Core.Entidade {

    public class Inimigo : Personagem {

        private Jogador alvo;
        private float ataqueCooldown = 2f;
        private float timerCooldown = 0f;
        [SerializeField] private GameObject barraHP;
        public Image hpVerde;
    
        protected override void Start(){
            base.Start();
            maxHP = 10;
            pontosVida = 10;
            ataque = 2;
            podeAtacar = false;
            hpVerde = barraHP.transform.Find("HP Base/HP").GetComponent<Image>();
        }

        protected override void Update(){
            base.Update();
            InimigoPodeAtacar();
            hpVerde.fillAmount = (float)pontosVida/maxHP;
            if(pontosVida <= 0) GetComponent<BoxCollider2D>().enabled = false;
        }

        private void InimigoPodeAtacar(){
            if(podeAtacar && alvo != null){

                if(alvo.pontosVida <= 0 || pontosVida <= 0){
                    podeAtacar = false;
                    return;
                }
                IntervaloAtaqueInimigo();
            }
        }

        private void IntervaloAtaqueInimigo(){
            timerCooldown -= Time.deltaTime;
            if (timerCooldown <= 0) {
                timerCooldown = ataqueCooldown;
                fachada.combateServico.Atacando(ataque, alvo, pontosEnergia);
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