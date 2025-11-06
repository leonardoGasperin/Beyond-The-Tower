using UnityEngine;
using btt.Aplicacao.DI.Personagem;
using DialogueSystem;

namespace btt.Core.Entidade {
    public class MiniBoss : Personagem
    {
        public GameObject projetil;
        private Jogador jogador;
        public Vector3 offset;
        private bool podeAtirar;
        public float distancia;
        private float tiroCooldown = 2f;
        private float timer = 0f;
        
        protected override void Start(){
            base.Start();
            jogador = GameObject.FindGameObjectWithTag("Jogador").GetComponent<Jogador>();
            podeAtirar = false;
            velocidade = 0;
            maxHP = 100;
            pontosVida = 100;
        }

        protected override void Update(){
            base.Update();

            if (pontosVida <= 0) {
                podeAtirar = false;
                return;
            }

            distancia = jogador.transform.position.x - transform.position.x;
            if (Mathf.Abs(distancia) > 0) {
                IntervaloTiro();
            }
        }

        private void IntervaloTiro(){
            timer -= Time.deltaTime;
            if (timer <= 0 && podeAtirar) {
                timer = tiroCooldown;
                InstanciarTiro();
            }
        }

        private void InstanciarTiro() {
            if(distancia > 0) {
                offset = transform.position + new Vector3(1, 0, 0);
                Instantiate(projetil, offset, Quaternion.identity);
            }
            else {
                offset = transform.position + new Vector3(-1, 0, 0);
                Instantiate(projetil, offset, Quaternion.identity);
            }
        }

        protected override void OnCollisionEnter2D(Collision2D col){
            base.OnCollisionEnter2D(col);
            if(col.gameObject.CompareTag("Jogador")){
                podeAtirar = false;
            }
        }

        protected override void OnCollisionExit2D(Collision2D col){
            base.OnCollisionExit2D(col);
            if(col.gameObject.CompareTag("Jogador")){
                podeAtirar = true;
            }
        }
    }
}