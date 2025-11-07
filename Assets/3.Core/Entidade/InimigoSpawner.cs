using UnityEngine;
using btt.Aplicacao.DI.Personagem;

namespace btt.Core.Entidade {

    public class InimigoSpawner : MonoBehaviour
    {
        public GameObject prefab;
        public Transform posicao;
        private PersonagemConfiguracaoDI.ServiceLocator fachada;
        private float timerCooldown = 0f;
        private float spawnCooldown = 2f;
        private Jogador jogador;

        void Start()
        {
            fachada = GetComponent<PersonagemConfiguracaoDI>().Services;
            jogador = GameObject.FindGameObjectWithTag("Jogador").GetComponent<Jogador>();
        }

        void Update()
        {
            if (transform.childCount == 0 && jogador.transform.position.x - 2 < transform.position.x) {
                IntervaloSpawn();
            }
        }

        public void IntervaloSpawn(){
            timerCooldown -= Time.deltaTime;
                if (timerCooldown <= 0) {
                    timerCooldown = spawnCooldown;
                    fachada.spawnInimigoServico.SpawnInimigo(prefab, posicao);
                }
        }
    }
}