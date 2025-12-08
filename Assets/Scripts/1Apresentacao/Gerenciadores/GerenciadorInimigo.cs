using btt.Core.Entidade;
using System.Collections.Generic;
using UnityEngine;

namespace btt.Apresentacao.Gerenciadores.InimigoGerenciador
{
    public class GerenciadorInimigo : MonoBehaviour
    {
        public static GerenciadorInimigo Instance { get; private set; }

        [Header("Configuração de Spawn")]
        [SerializeField] private GameObject prefabInimigo;
        [SerializeField] private Transform[] pontoSpawns;

        private readonly Queue<GameObject> poolInimigos = new();

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
            SpawnInimigos();
        }

        /// <summary>
        /// Spawna lista de inimigos.
        /// </summary>
        public void SpawnInimigos()
        {
            if (pontoSpawns.Length == 0) return;

            foreach (var ponto in pontoSpawns)
                Instantiate(prefabInimigo.GetComponent<Inimigo>(), ponto.position, Quaternion.identity);
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
