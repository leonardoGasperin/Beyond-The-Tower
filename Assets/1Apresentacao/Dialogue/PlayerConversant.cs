using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace DialogueSystem {

	public class PlayerConversant : MonoBehaviour {
		
		[SerializeField] string playerName;
		[SerializeField] Dialogue testDialogue; // no Inspector, vincular o arquivo do diálogo no slot
		Dialogue currentDialogue;
		DialogueNode currentNode = null;
		AIConversant currentConversant = null;
		bool isChoosing = false;

		public event Action onConversationUpdated;
        /*
		IEnumerator Start(){
			yield return new WaitForSeconds(2);
			StartDialogue(testDialogue);
		}
        */
		public void StartDialogue(AIConversant newConversant, Dialogue newDialogue){
			currentConversant = newConversant;
			currentDialogue = newDialogue;
			currentNode = currentDialogue.GetRootNode();
			TriggerEnterAction();
			//onConversationUpdated();
            onConversationUpdated?.Invoke();
		}

		public void Quit(){
			currentDialogue = null;
			TriggerExitAction();
			currentNode = null;
			isChoosing = false;
			currentConversant = null;
            onConversationUpdated?.Invoke();
			//onConversationUpdated();
		}

		public bool IsActive(){
			return currentDialogue != null;
		}

		public bool IsChoosing(){
			return isChoosing;
		}

		public string GetText(){
			if(currentNode == null) {
				return "";
			}
			return currentNode.GetText();
		}

		public string GetCurrentConversantName(){
			if (currentNode == null) return "";

			// If the current node belongs to the player, show the player's name
			if (currentNode.IsPlayerSpeaking())
			{
				return playerName;
			}

			// Otherwise, show the NPC's name
			return currentConversant != null ? currentConversant.GetName() : "";
		}

		public IEnumerable<DialogueNode> GetChoices(){
			return currentDialogue.GetPlayerChildren(currentNode);
		}

		public void SelectChoice(DialogueNode chosenNode){
			currentNode = chosenNode;
			TriggerEnterAction();
			isChoosing = false;
			Next();
		}

		public void Next()
		{
			if (currentNode == null || currentDialogue == null)
			{
				Quit();
				return;
			}

			// ============================
			// PLAYER NODE
			// ============================
			if (currentNode.IsPlayerSpeaking())
			{
				// If this player node IS a choice node → show buttons
				if (currentNode.IsChoiceNode())
				{
					var choices = currentDialogue.GetPlayerChildren(currentNode).ToArray();
					if (choices.Length > 0)
					{
						isChoosing = true;
						TriggerExitAction();
						onConversationUpdated?.Invoke();
						return;
					}
				}

				// Otherwise, non-choice player node
				DialogueNode[] nextChildren = currentDialogue.GetAllChildren(currentNode).ToArray();
				if (nextChildren.Length == 0)
				{
					Quit();
					return;
				}

				// Check if next nodes are player choice nodes
				bool hasChoiceChildren = false;
				foreach (var child in nextChildren)
				{
					if (child.IsPlayerSpeaking() && child.IsChoiceNode())
					{
						hasChoiceChildren = true;
						break;
					}
				}

				if (hasChoiceChildren)
				{
					// Stay on current node and show the upcoming choices
					isChoosing = true;
					TriggerExitAction();
					onConversationUpdated?.Invoke();
					return;
				}
				else
				{
					// Otherwise, just advance to the next node normally
					TriggerExitAction();
					currentNode = nextChildren[0];
					TriggerEnterAction();
					onConversationUpdated?.Invoke();
					return;
				}
			}

			// ============================
			// AI NODE
			// ============================
			DialogueNode[] playerChildren = currentDialogue.GetPlayerChildren(currentNode).ToArray();

			if (playerChildren.Length > 0)
			{
				// Single non-choice player line → auto advance
				if (playerChildren.Length == 1 && !playerChildren[0].IsChoiceNode())
				{
					TriggerExitAction();
					currentNode = playerChildren[0];
					TriggerEnterAction();
					onConversationUpdated?.Invoke();
					return;
				}

				// Multiple player options or a choice node → show buttons
				isChoosing = true;
				TriggerExitAction();
				onConversationUpdated?.Invoke();
				return;
			}

			// ============================
			// AI → AI transition or end
			// ============================
			DialogueNode[] aiChildren = currentDialogue.GetAIChildren(currentNode).ToArray();
			if (aiChildren.Length == 0)
			{
				Quit();
				return;
			}

			int aiIndex = UnityEngine.Random.Range(0, aiChildren.Length);
			TriggerExitAction();
			currentNode = aiChildren[aiIndex];
			TriggerEnterAction();
			onConversationUpdated?.Invoke();
		}


		public bool HasNext(){
			return currentDialogue.GetAllChildren(currentNode).Count() > 0;
		}

		private void TriggerEnterAction(){
			if(currentNode != null){
				TriggerAction(currentNode.GetOnEnterAction());
			}
		}

		private void TriggerExitAction(){
			if(currentNode != null){
				TriggerAction(currentNode.GetOnExitAction());
			}
		}

		private void TriggerAction(string action){
			if(action == "") return;
			foreach(DialogueTrigger trigger in currentConversant.GetComponents<DialogueTrigger>()) {
				trigger.Trigger(action);
			}
		}

        public AIConversant GetCurrentConversant()
        {
            return currentConversant;
        }
	}
}
