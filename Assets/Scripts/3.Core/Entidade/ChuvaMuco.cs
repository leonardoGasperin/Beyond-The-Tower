using UnityEngine;
using btt.Aplicacao.DI.Personagem;

namespace btt.Core.Entidade
{
    public class ChuvaMuco : MonoBehaviour
    {
        private PersonagemConfiguracaoDI.ServiceLocator fachada;
        private Jogador alvo;
        public int ataque = 2;
        public GameObject sombraPrefab;
        private GameObject sombraInstanciada;
        private int pontosEnergia;

        void Start()
        {
            fachada = GetComponent<PersonagemConfiguracaoDI>().Services;
            alvo = GameObject.Find("Jogador").GetComponent<Jogador>();
            Vector3 inicioRaycast = transform.position + Vector3.down * 1f;

            RaycastHit2D hit = Physics2D.Raycast(inicioRaycast, Vector2.down, 15.0f);
            if (hit.collider != null && hit.collider.CompareTag("Plataforma"))
            {
                Vector2 sombraPosicao = new Vector2(transform.position.x, hit.point.y + 0.1f);
                sombraInstanciada = Instantiate(sombraPrefab, sombraPosicao, Quaternion.identity);
            }
        }

        void Update()
        {
            Debug.DrawRay(transform.position, Vector2.down * 15f, Color.yellow);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Jogador"))
            {
                fachada.combateServico.Atacando(ataque, alvo);
                Destroy(gameObject);
                Destroy(sombraInstanciada);
            }
            else if (col.gameObject.CompareTag("Parede") || col.gameObject.CompareTag("Plataforma") || col.gameObject.CompareTag("Inimigo"))
            {
                Destroy(gameObject);
                Destroy(sombraInstanciada);
            }
        }
    }
}