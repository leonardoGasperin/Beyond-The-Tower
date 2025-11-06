using UnityEngine;
using btt.Aplicacao.DI.Personagem;

namespace btt.Core.Entidade {

    public class Projetil : MonoBehaviour
    {
        private PersonagemConfiguracaoDI.ServiceLocator fachada;
        private Vector2 velocidadeTiro;
        private Jogador alvo;
        public int ataque;
        private int pontosEnergia;
        private float distanciaDoJogador;
        private float gravidade;
        private bool chao;
        private float velocidadeHorizontal;
        private float velocidadeVertical;
        private float tempoNoAr;

        void Start()
        {
            fachada = GetComponent<PersonagemConfiguracaoDI>().Services;
            alvo = GameObject.Find("Jogador").GetComponent<Jogador>();
            ataque = 2;
            distanciaDoJogador = alvo.transform.position.x - transform.position.x;
            gravidade = -9.8f;
            chao = false;
            tempoNoAr = 1f;
            velocidadeHorizontal = distanciaDoJogador / tempoNoAr;
            velocidadeVertical = gravidade * tempoNoAr;
            velocidadeTiro = new Vector2 (velocidadeHorizontal, velocidadeVertical);
        }

        void Update()
        {

            if(!chao){
                velocidadeTiro.y += gravidade * Time.deltaTime;
                fachada.atiraServico.Atira(velocidadeTiro, transform);
            }
            
        }

        private void OnTriggerEnter2D(Collider2D col){
            if(col.gameObject.CompareTag("Jogador")) {
                fachada.combateServico.Atacando(ataque, alvo, pontosEnergia);
            } else if(col.gameObject.CompareTag("Plataforma")) {
                ataque = 0;
                chao = true;
                velocidadeTiro = Vector2.zero;
            }
        }

    }

}
