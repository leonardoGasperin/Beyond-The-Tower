using UnityEngine;
using btt.Configuracao.DI.Personagem;

namespace btt.Core.Entidade
{
    public class TiroMuco : MonoBehaviour
    {
        /// TODO: Analisar codigo de Projetil e TiroMuco e ChuvaMuco para ver o que pode ser reaproveitado e criar uma classe base
        private PersonagemConfiguracaoDI.ServiceLocator fachada;
        public float velocidadeTiro;
        private Jogador alvo;
        public int ataque;
        public float direcao;

        void Start()
        {
            fachada = GetComponent<PersonagemConfiguracaoDI>().Services;
            alvo = GameObject.Find("Jogador").GetComponent<Jogador>();
            ataque = 2;
            velocidadeTiro = 10f;
        }

        void Update()
        {
            fachada.movimentacaoServico.Movimentacao(transform, velocidadeTiro, direcao);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Jogador"))
            {
                fachada.combateServico.Atacando(ataque, alvo);
                Destroy(gameObject);
            }
            else if (col.gameObject.CompareTag("Parede") || col.gameObject.CompareTag("Plataforma"))
                Destroy(gameObject);
        }
    }
}