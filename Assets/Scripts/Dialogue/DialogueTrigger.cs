using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DialogueSystem {

	public class DialogueTrigger : MonoBehaviour {
		[SerializeField] string action; // o string que terá no inspector deverá ser igual ao que foi escrito no serialize field on exit action do script DialogueNode
		[SerializeField] UnityEvent onTrigger; // no inspector, arrastar no slot o objeto que contém o script que contém a função a ser engatilhada

		public void Trigger(string actionToTrigger){
			if(actionToTrigger == action){
				onTrigger.Invoke();
			}
		}
	}
}
