using UnityEngine;
using btt.Core.Entidade;

namespace btt.Aplicacao.Handler.CheckpointHandler
{
    public class Checkpoint : MonoBehaviour
    {
        private GerenciadorCamera mainCamera;
        private CanvasGroup energiasCanvasGroup;

        private void Start()
        {
            mainCamera = FindFirstObjectByType<GerenciadorCamera>();
            energiasCanvasGroup = GameObject.Find("Energias").GetComponent<CanvasGroup>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Jogador jogador = other.GetComponent<Jogador>();
            if (jogador != null)
            {
                Save.SalvarEstadoJogo(jogador.gameObject, mainCamera, energiasCanvasGroup);
                Debug.Log("Checkpoint alcançado");
            }
        }
    }

}