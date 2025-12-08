using btt.Aplicacao.DI.Spawner;
using UnityEngine;

namespace btt.Core.Entidade
{

    public class Spawner : MonoBehaviour
    {
        private SpawnerConfiguracaoDI.ServiceLocator fachada;

        public GameObject prefab;
        public Transform posicao;
        public bool spawnDireita;
        public float desvio;
        public float spawnCooldown = 2f;
        private Jogador jogador;
        private float timerCooldown = 0f;

        void Start()
        {
            fachada = GetComponent<SpawnerConfiguracaoDI>().Services;
            jogador = GameObject.FindGameObjectWithTag("Jogador").GetComponent<Jogador>();
        }

        void Update()
        {
            if (transform.childCount == 0 && spawnDireita && jogador.transform.position.x + desvio > transform.position.x)
            {
                IntervaloSpawn();
            }
            else if (transform.childCount == 0 && !spawnDireita && jogador.transform.position.x - desvio < transform.position.x)
            {
                IntervaloSpawn();
            }
        }

        public void IntervaloSpawn()
        {
            timerCooldown -= Time.deltaTime;
            if (timerCooldown <= 0)
            {
                timerCooldown = spawnCooldown;
                fachada.spawnServico.Spawn(prefab, posicao);
            }
        }
    }
}