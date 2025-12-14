using UnityEngine;

namespace btt.Aplicacao.Handler.SpawnerHandler
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField]private float timerCooldown;
        
        public GameObject prefab;
        public Transform posicao;
        public int spawnCooldown;
        public bool infinito;
        public bool podeSpawnar;

        void Start()
        {
            timerCooldown = 10f;
        }

        void FixedUpdate()
        {
            timerCooldown -= Time.deltaTime;
        }

        private void OnTriggerStay2D(Collider2D col)
        {
            if (timerCooldown > 0 || !col.CompareTag("Jogador")) return;
            podeSpawnar = true;
            timerCooldown = spawnCooldown;
        }

    }
}