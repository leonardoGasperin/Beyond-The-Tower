using btt.Core.Entidade;
using UnityEngine;

namespace DialogueSystem
{
    public class AIConversant : MonoBehaviour
    {

        [SerializeField] string conversantName;
        [SerializeField] Dialogue dialogue = null;
        private GerenciadorCamera mainCamera;
        private Jogador jogador;
        public bool dialogoCompleto = false;
        private bool dialogoComecou = false; 

        void Start()
        {
            mainCamera = FindFirstObjectByType<GerenciadorCamera>();
            jogador = FindFirstObjectByType<Jogador>();
        }

        public void OnCollisionEnter2D(Collision2D col)
        {
            if (gameObject.tag != "npc" && !col.gameObject.CompareTag("Jogador") || dialogoCompleto) return;
 
            col.gameObject.GetComponent<PlayerConversant>().StartDialogue(this, dialogue);
            mainCamera.cameraPodeSubir = false;
            dialogoComecou = true;
            jogador.emDialogo = dialogoComecou;
        }

        public string GetName()
            => conversantName;

        public void DialogoCompleto()
        {
            dialogoCompleto = true;
            dialogoComecou = false;
            jogador.emDialogo = dialogoComecou;
        }
    }
}
