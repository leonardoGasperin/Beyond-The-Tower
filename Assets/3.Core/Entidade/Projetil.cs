using UnityEngine;
using btt.Aplicacao.DI.Personagem;

namespace btt.Core.Entidade {

    public class Projetil : MonoBehaviour
    {
        private PersonagemConfiguracaoDI.ServiceLocator fachada;
        public Vector2 velocidadeTiro;
        private Jogador alvo;
        public int ataque;
        private int pontosEnergia;
        public float gravidade;
        public bool chao;

        void Start()
        {
            fachada = GetComponent<PersonagemConfiguracaoDI>().Services;
            alvo = GameObject.Find("Jogador").GetComponent<Jogador>();
            ataque = 2;
            gravidade = -9.8f;
            chao = false;
        }

        void Update()
        {
            if(!chao){
                velocidadeTiro.y += gravidade * Time.deltaTime;
                fachada.lancaServico.MovimentoLanca(velocidadeTiro, transform);
            }
        }

        private void OnTriggerEnter2D(Collider2D col){
            if(col.gameObject.CompareTag("Jogador")) {
                fachada.combateServico.Atacando(ataque, alvo, pontosEnergia);
            } else if(col.gameObject.CompareTag("Plataforma")) {
                velocidadeTiro = Vector2.zero;
                ataque = 0;
                chao = true;
            }
        }
    }
}
