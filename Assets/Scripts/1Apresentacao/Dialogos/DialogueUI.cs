using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystem;
using TMPro;
using UnityEngine.UI;
using btt.Core.Entidade;
using System.Threading.Tasks;

namespace UI {
	public class DialogueUI : MonoBehaviour {

        private SairDialogo sairDialogo;
		PlayerConversant playerConversant;
		[SerializeField] private RectTransform dialogueBox; // 👈 assign your main container here
		[SerializeField] TextMeshProUGUI AIText; // corpo do texto que a IA irá falar. Vincular no slot que aparecerá no Inspector
		[SerializeField] Button nextButton;
		[SerializeField] GameObject AIResponse; // vincular de dentro do prefab de diálogo o container com o texto da IA
		[SerializeField] Transform choiceRoot; // vincular no slot o game object que tem os botões de escolha
		[SerializeField] GameObject choicePrefab; // vincular no slot o prefab do botão que será a opção em si
		[SerializeField] Button quitButton; // vincular no slot o botão de fechar
		[SerializeField] TextMeshProUGUI conversantName; // Na hierarquia, arrastar o objeto do current speaker para o slot no inspector
        public int pontosAltruismo;
        public int pontosEgoismo;

		void Start(){
            sairDialogo = FindFirstObjectByType<SairDialogo>();
			playerConversant = GameObject.FindGameObjectWithTag("Jogador").GetComponent<PlayerConversant>();
			playerConversant.onConversationUpdated += UpdateUI;
			nextButton.onClick.AddListener(() => playerConversant.Next());
			quitButton.onClick.AddListener(() => {
                playerConversant.Quit();
                sairDialogo.CameraSobe();
            });
			UpdateUI();
		}
	
		private async void UpdateUI(){
			gameObject.SetActive(playerConversant.IsActive());
			if(!playerConversant.IsActive()) {
				return;
			}

			conversantName.text = playerConversant.GetCurrentConversantName();
			
			AIResponse.SetActive(!playerConversant.IsChoosing());
			choiceRoot.gameObject.SetActive(playerConversant.IsChoosing());
			
			if(playerConversant.IsChoosing()){
				BuildChoiceList();
                quitButton.gameObject.SetActive(false);
			}
			else {
				AIText.text = playerConversant.GetText();
				nextButton.gameObject.SetActive(playerConversant.HasNext());
                quitButton.gameObject.SetActive(!playerConversant.HasNext());
			}

			await ForceRebuildLayoutAsync();
		}

		private async Task ForceRebuildLayoutAsync() {
			await Task.Yield(); // wait one frame
			Canvas.ForceUpdateCanvases();
			if (dialogueBox != null)
				LayoutRebuilder.ForceRebuildLayoutImmediate(dialogueBox);
		}

		
		private void BuildChoiceList(){
			foreach(Transform item in choiceRoot){
				Destroy(item.gameObject);
			}
            int index = 0;
			foreach(DialogueNode choice in playerConversant.GetChoices()) {
                int botaoEscolha = index;
				GameObject choiceInstance = Instantiate(choicePrefab, choiceRoot);
				var textComp = choiceInstance.GetComponentInChildren<TextMeshProUGUI>();
				textComp.text = choice.GetText();
				Button button = choiceInstance.GetComponentInChildren<Button>();
				button.onClick.AddListener(() => {
                    if(botaoEscolha == 0){
                        pontosAltruismo++;
                        Debug.Log(pontosAltruismo);
                    } else if (botaoEscolha == 1) {
                        pontosEgoismo++;
                        Debug.Log(pontosEgoismo);
                    }
					playerConversant.SelectChoice(choice);
				});
                index++;
			}
		}
	}
}
