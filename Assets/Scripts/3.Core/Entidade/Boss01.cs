using UnityEngine;
using btt.Aplicacao.DI.Personagem;
using UnityEngine.UI;

namespace btt.Core.Entidade {

    public class Boss01 : Personagem {

        private Transform barraHP;
        public Image hpVerde;
        public Jogador jogador;
        public GameObject tiroMucoPrefab;
        private float distancia;
        private float timerCooldown = 0f;

        private int danoTiroMuco = 2;
        public float tiroMucoCooldown = 3f;
        private bool podeAtirarMuco = true;

        protected override void Start(){
            base.Start();
            tagAlvo = "Jogador";
            maxHP = 100;
            pontosVida = 100;
            barraHP = transform.Find("Barra de HP");
            hpVerde = barraHP.transform.Find("HP Base/HP").GetComponent<Image>();
            jogador = GameObject.FindGameObjectWithTag("Jogador").GetComponent<Jogador>();
            podeAndar = false;
        }

        protected override void Update(){
            base.Update();
            hpVerde.fillAmount = (float)pontosVida/maxHP;

            distancia = transform.position.x - jogador.transform.position.x;
            if(distancia > 0) {
                transform.localScale = new Vector3(1, 1, 1);
                barraHP.transform.localRotation = Quaternion.Euler(0, 0, 0);
            } else {
                transform.localScale = new Vector3(-1, 1, 1);
                barraHP.transform.localRotation = Quaternion.Euler(0, 180, 0);
            }

            if(podeAtirarMuco) {
                IntervaloTiroMuco();
            }
        }

        private void IntervaloTiroMuco(){
            timerCooldown -= Time.deltaTime;
            if (timerCooldown <= 0) {
                timerCooldown = tiroMucoCooldown;
                //Instantiate(tiroMucoPrefab, );
            }
        }
    }
}