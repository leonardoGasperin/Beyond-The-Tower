using UnityEngine;
using btt.Core.Entidade;
using DialogueSystem;
using System.Threading.Tasks;
using btt.Aplicacao.DI.Personagem;

public class SairDialogo : MonoBehaviour
{
    private GerenciadorCamera mainCamera;
    private Jogador jogador;
    private AIConversant interlocutor;
    [SerializeField] private GameObject prefabInimigo;
    private CanvasGroup energiasCanvasGroup;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = FindFirstObjectByType<GerenciadorCamera>();   
        jogador = FindFirstObjectByType<Jogador>();
        interlocutor = FindFirstObjectByType<PlayerConversant>().GetCurrentConversant();
        energiasCanvasGroup = GameObject.Find("Energias").GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CameraSobe(){
        Retomar();
        if (interlocutor != null)
        {
            interlocutor.DialogoCompleto();
        }
    }

    public void FimTutorial(){
        CameraSobe();
        energiasCanvasGroup.alpha = 1f;
        jogador.pontosEnergia = 3;
    }

    public async void Expurgar(){
        Retomar();
        AIConversant npcAtual = FindFirstObjectByType<PlayerConversant>().GetCurrentConversant();
        GameObject inimigoInstanciado = Instantiate(prefabInimigo, npcAtual.transform.position, npcAtual.transform.rotation);
        Inimigo inimigoScript = inimigoInstanciado.GetComponent<Inimigo>();
        await Task.Yield();
        inimigoScript.pontosVida = 0;
        DestruirNPC(npcAtual);
    }

    public void Lutar(){
        Retomar();
        AIConversant npcAtual = FindFirstObjectByType<PlayerConversant>().GetCurrentConversant();
        Instantiate(prefabInimigo, npcAtual.transform.position, npcAtual.transform.rotation);
        DestruirNPC(npcAtual);
    }

    private async void DestruirNPC(AIConversant npc)
    {
        await Task.Delay(1);
        if (npc != null) Destroy(npc.gameObject);
    }

    private void Retomar(){
        mainCamera.cameraVelocidade = 1f;
        jogador.velocidade = 5f;
    }
}
