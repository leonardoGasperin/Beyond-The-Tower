using btt.Aplicacao.DI.Spawner;
using UnityEngine;

namespace btt.Core.Entidade
{

    public class Spawner : MonoBehaviour
    {
        private SpawnerConfiguracaoDI.ServiceLocator fachada;

        public GameObject prefab;
        public Transform posicao;
        public float desvio;
        public float spawnCooldown;

        private Jogador jogador;
        private float timerCooldown;

        void Start()
        {
            spawnCooldown = 10f;
            timerCooldown = 10f;
            fachada = GetComponent<SpawnerConfiguracaoDI>().Services;
            jogador = GameObject.FindGameObjectWithTag("Jogador").GetComponent<Jogador>();
        }

        void FixedUpdate()
        {
            timerCooldown -= 1*Time.deltaTime;
        }

        private void OnTriggerStay2D(Collider2D col)
        {
            if (timerCooldown > 0) return;

            if (col.CompareTag("Jogador"))
                IntervaloSpawn();
        }

        public void IntervaloSpawn()
        {
            timerCooldown = spawnCooldown;
            fachada.spawnServico.Spawn(prefab, posicao);
        }
    }
}