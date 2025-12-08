using UnityEngine;
using btt.Aplicacao.DI.Personagem;
using btt.Aplicacao.DI.Inimigo;

namespace btt.Core.Entidade {

    public class Projetil : MonoBehaviour
    {
        private PersonagemConfiguracaoDI.ServiceLocator fachada;
        private InimigoConfiguracaoDI.ServiceLocator fachadaInimigo;
        public Vector2 velocidadeTiro;
        private Jogador alvo;
        public int ataque;
        private int pontosEnergia;
        public float gravidade;
        public bool chao;

        void Start()
        {
            fachada = GetComponent<PersonagemConfiguracaoDI>().Services;
            fachadaInimigo = GetComponent<InimigoConfiguracaoDI>().Services;
            alvo = GameObject.Find("Jogador").GetComponent<Jogador>();
            ataque = 2;
            gravidade = -9.8f;
            chao = false;
        }

        void Update()
        {
            if(!chao){
                velocidadeTiro.y += gravidade * Time.deltaTime;
                fachadaInimigo.lancaServico.MovimentoLanca(velocidadeTiro, transform);
            }
        }

        private void OnTriggerEnter2D(Collider2D col){
            if(col.gameObject.CompareTag("Jogador")) {
                fachada.combateServico.Atacando(ataque, alvo);
            } else if(col.gameObject.CompareTag("Plataforma")) {
                velocidadeTiro = Vector2.zero;
                ataque = 0;
                chao = true;
            } else if(col.gameObject.CompareTag("Parede")) {
                velocidadeTiro.x = 0;
            }
        }
    }
}
