using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using btt.Core.Entidade;

namespace DialogueSystem {

    public class AIConversant : MonoBehaviour {

        [SerializeField] string conversantName;
        [SerializeField] Dialogue dialogue = null; // vincular o arquivo de diálogo no slot
        private GerenciadorCamera mainCamera;
        private Jogador jogador;
        public bool dialogoCompleto = false;
        private bool dialogoComecou = false;

        /*
        public CursorType GetCursorType(){
            return CursorType.Dialogue;
        }
        
        public bool HandleRaycast(PlayerController callingController){
            if(dialogue == null){
                return false;
            }
            if(Input.GetMouseButtonDown(0)){
                callingController.GetComponent<PlayerConversant>().StartDialogue(this, dialogue);
            }
            return true;
        }
        */

        void Start()
        {
            mainCamera = FindFirstObjectByType<GerenciadorCamera>();
            jogador = FindFirstObjectByType<Jogador>();
        }

        public void OnCollisionEnter2D(Collision2D col) {
            if(dialogue == null || dialogoCompleto || dialogoComecou){
                //return false;
                return;
            }
            if(gameObject.tag == "npc" && col.gameObject.CompareTag("Jogador")) {
                col.gameObject.GetComponent<PlayerConversant>().StartDialogue(this, dialogue);
                mainCamera.cameraVelocidade = 0;
                jogador.anda = false;
                jogador.podePular = false;
                dialogoComecou = true;
            }
            //return true;
        }

        private void Update(){

            if(dialogoComecou || dialogoCompleto || dialogue == null) return;

            Vector2 origem = (Vector2)transform.position + Vector2.left * 1f;
            Vector2 direcao = Vector2.left;
            RaycastHit2D hit = Physics2D.Raycast(origem, direcao, 100f);
            
            if (hit.collider != null && gameObject.GetComponent<MiniBoss>() != null && hit.collider.tag == "Jogador"){
                hit.collider.gameObject.GetComponent<PlayerConversant>().StartDialogue(this, dialogue);
                jogador.anda = false;
                jogador.podePular = false;
                dialogoComecou = true;
            }   
        }

        public string GetName(){
            return conversantName;
        }

        public void DialogoCompleto(){
            dialogoCompleto = true;
        }
    }
}
