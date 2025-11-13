using UnityEngine;
using btt.Aplicacao.DI.Personagem;

namespace btt.Core.Entidade {

    public class Spawner : MonoBehaviour
    {
        public GameObject prefab;
        public Transform posicao;
        public bool spawnDireita;
        public float desvio;
        private PersonagemConfiguracaoDI.ServiceLocator fachada;
        private float timerCooldown = 0f;
        public float spawnCooldown = 2f;
        private Jogador jogador;

        void Start()
        {
            fachada = GetComponent<PersonagemConfiguracaoDI>().Services;
            jogador = GameObject.FindGameObjectWithTag("Jogador").GetComponent<Jogador>();
        }

        void Update()
        {   
            if(transform.childCount == 0 && spawnDireita && jogador.transform.position.x + desvio > transform.position.x) {
                IntervaloSpawn();
            } else if (transform.childCount == 0 && !spawnDireita && jogador.transform.position.x - desvio < transform.position.x) {
                IntervaloSpawn();
            }
        }

        public void IntervaloSpawn(){
            timerCooldown -= Time.deltaTime;
                if (timerCooldown <= 0) {
                    timerCooldown = spawnCooldown;
                    fachada.spawnServico.Spawn(prefab, posicao);
                }
        }
    }
}