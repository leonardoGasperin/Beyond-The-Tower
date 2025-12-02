using UnityEngine;
using btt.Aplicacao.DI.Personagem;

namespace btt.Core.Entidade {
    public class TiroMuco : MonoBehaviour {

        private PersonagemConfiguracaoDI.ServiceLocator fachada;
        public float velocidadeTiro;
        private Jogador alvo;
        public int ataque;
        private int pontosEnergia;
        public float direcao;
        
        void Start() {
            fachada = GetComponent<PersonagemConfiguracaoDI>().Services;
            alvo = GameObject.Find("Jogador").GetComponent<Jogador>();
            ataque = 2;
            velocidadeTiro = 10f;
        }
        
        void Update() {
            fachada.movimentacaoServico.Movimentacao(transform, velocidadeTiro, direcao);
        }

        private void OnCollisionEnter2D(Collision2D col){
            if(col.gameObject.CompareTag("Jogador")) {
                fachada.combateServico.Atacando(ataque, alvo, pontosEnergia);
                Destroy(gameObject);
            } else if(col.gameObject.CompareTag("Parede") || col.gameObject.CompareTag("Plataforma")) {
                Destroy(gameObject);
            }
        }
    }
}