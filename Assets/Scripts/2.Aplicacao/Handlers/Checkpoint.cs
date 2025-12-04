using UnityEngine;
using btt.Core.Entidade;

public class Checkpoint : MonoBehaviour
{
    private GerenciadorCamera mainCamera;
    private CanvasGroup energiasCanvasGroup;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = FindFirstObjectByType<GerenciadorCamera>();
        energiasCanvasGroup = GameObject.Find("Energias").GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
