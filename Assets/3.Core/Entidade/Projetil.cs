using UnityEngine;
using btt.Aplicacao.DI.Personagem;

namespace btt.Core.Entidade {

    public class Projetil : MonoBehaviour
    {
        private PersonagemConfiguracaoDI.ServiceLocator fachada;
        float velocidadeTiro;
        private Jogador alvo;
        public int ataque;
        private int pontosEnergia;
        private Vector3 direcaoTiro;
        private float distanciaDoJogador;

        void Start()
        {
            fachada = GetComponent<PersonagemConfiguracaoDI>().Services;
            alvo = GameObject.Find("Jogador").GetComponent<Jogador>();
            velocidadeTiro = 6f;
            ataque = 2;
            distanciaDoJogador = alvo.transform.position.x - transform.position.x;
        }

        void Update()
        {
            if (distanciaDoJogador > 0) {
                direcaoTiro = Vector3.right;
            }
            else {
                direcaoTiro = Vector3.left;
            }
            
            fachada.atiraServico.Atira(direcaoTiro, velocidadeTiro, transform);
        }

        private void OnCollisionEnter2D(Collision2D col){
            if(col.gameObject.CompareTag("Jogador")) fachada.combateServico.Atacando(ataque, alvo, pontosEnergia);
            Destroy(gameObject);
        }

    }

}
