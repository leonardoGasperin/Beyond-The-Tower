using UnityEngine;
using btt.Core.Entidade;
using DialogueSystem;
using System.Threading.Tasks;
using btt.Configuracao.DI.Personagem;

public class SairDialogo : MonoBehaviour
{
    private GerenciadorCamera mainCamera;
    private Jogador jogador;
    private AIConversant interlocutor;
    [SerializeField] private GameObject prefabInimigo;
    private CanvasGroup energiasCanvasGroup;
    private MiniBoss miniBoss;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = FindFirstObjectByType<GerenciadorCamera>();   
        jogador = FindFirstObjectByType<Jogador>();
        miniBoss = FindFirstObjectByType<MiniBoss>();
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

    private void Retomar(){
        mainCamera.cameraPodeSubir = true;
        jogador.anda = true;
        jogador.podePular = true;
    }

    public void FimTutorial(){
        CameraSobe();
        energiasCanvasGroup.alpha = 1f;
        jogador.pontosEnergia = 3;
        jogador.podePular = true;
        jogador.anda = true;
    }

    public async void Expurgar(){
        Retomar();
        AIConversant npcAtual = FindFirstObjectByType<PlayerConversant>().GetCurrentConversant();
        GameObject inimigoInstanciado = Instantiate(prefabInimigo, npcAtual.transform.position, npcAtual.transform.rotation);
        Inimigo inimigoScript = inimigoInstanciado.GetComponent<Inimigo>();
        await Task.Yield();
        inimigoScript.pontosVida = 0;
        DestruirNPC(npcAtual);
        jogador.podePular = true;
        jogador.anda = true;
    }

    public void Lutar(){
        Retomar();
        AIConversant npcAtual = FindFirstObjectByType<PlayerConversant>().GetCurrentConversant();
        Instantiate(prefabInimigo, npcAtual.transform.position, npcAtual.transform.rotation);
        DestruirNPC(npcAtual);
        jogador.podePular = true;
        jogador.anda = true;
    }

    private async void DestruirNPC(AIConversant npc)
    {
        await Task.Delay(1);
        if (npc != null) Destroy(npc.gameObject);
    }

    public void LutaMiniBoss(){
        miniBoss.podeAtirar = true;
        jogador.anda = true;
        jogador.podePular = true;
    }
}
