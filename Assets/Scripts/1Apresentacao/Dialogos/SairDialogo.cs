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

    public void CameraSobe(){
        Retomar();
        if (interlocutor != null)
        {
            interlocutor.DialogoCompleto();
        }
    }

    private void Retomar(){
        mainCamera.cameraPodeSubir = true;
        jogador.emDialogo = false;
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
        Destroy(npcAtual.gameObject);
    }

    public void Lutar(){
        Retomar();
        AIConversant npcAtual = FindFirstObjectByType<PlayerConversant>().GetCurrentConversant();
        Instantiate(prefabInimigo, npcAtual.transform.position, npcAtual.transform.rotation);
    }

    public void LutaMiniBoss()
        => miniBoss.podeAtirar = true;
}
