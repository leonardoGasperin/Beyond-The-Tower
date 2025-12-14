using btt.Aplicacao.Handler.SpawnerHandler;
using btt.Core.Entidade;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace btt.Apresentacao.Gerenciadores.InimigoGerenciador
{
    public class GerenciadorInimigo : MonoBehaviour
    {
        public static GerenciadorInimigo Instance { get; private set; }

        [Header("Configuração de Spawn")]
        [SerializeField] private Spawner[] spawners;
        private readonly Queue<GameObject> poolInimigos = new();

        private List<Spawner> Spawners => spawners.Where(e => e != null && !e.infinito).ToList();
        private List<Spawner> SpawnerInfinitos => spawners.Where(e => e != null && e.infinito).ToList();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }

        private void Start()
        {
            Spawn();
        }

        private void Update()
        {
            SpawnInfinito();
        }

        /// <summary>
        /// Spawna lista de inimigos.
        /// </summary>
        private void Spawn()
        {
            if (Spawners.Count == 0) return;
            Spawners
                .ForEach(spawn
                    => Instantiate(spawn.prefab.GetComponent<Inimigo>(), spawn.posicao.position, Quaternion.identity));
        }

        private void SpawnInfinito()
        {
            if (SpawnerInfinitos.Count == 0) return;
            SpawnerInfinitos
                .Where(e => e.podeSpawnar)
                .ToList()
                .ForEach(
                    spawn =>
                    {
                        Instantiate(spawn.prefab.GetComponent<Inimigo>(), spawn.posicao.position, Quaternion.identity);
                        spawn.podeSpawnar = false;
                    }
                );
        }

        /// <summary>
        /// Remove inimigo da cena e não libera memória.
        /// </summary>
        public void RemoverInimigo(Inimigo inimigo)
        {
            if (inimigo == null) return;

            inimigo.gameObject.SetActive(false);
            poolInimigos.Enqueue(inimigo.gameObject);
        }

        /// <summary>
        /// Destrói inimigo da cena e libera memória.
        /// </summary>
        public void DestruirInimigo(Inimigo inimigo)
        {
            if (inimigo == null) return;
            Destroy(inimigo.gameObject);
        }

    }
}
