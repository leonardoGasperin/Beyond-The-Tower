using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DialogueSystem {
	public class DialogueNode : ScriptableObject {
		[SerializeField]
		bool isPlayerSpeaking = false;
		[SerializeField]
		bool isChoiceNode = false;
		[SerializeField]
		string text; // A fala que o jogador ou o npc terá
		[SerializeField]
		List<string> children = new List<string>(); // lista de filhos, os quais serão referenciadas pelos IDs únicos
		[SerializeField]
		Rect rect = new Rect(0, 0, 200, 100);
        [SerializeField]
        string onEnterAction; // esses dois strings aparecerão no inspector quando clicar no nó de um diálogo
        [SerializeField]
        string onExitAction;

		public bool IsChoiceNode() {
			return isChoiceNode;
		}

		#if UNITY_EDITOR
		public void SetChoiceNode(bool value) {
			Undo.RecordObject(this, "Toggle Choice Node");
			isChoiceNode = value;
			EditorUtility.SetDirty(this);
		}
		#endif

		public Rect GetRect(){
			return rect;
		}

		public string GetText(){
			return text;
		}

		public List<string> GetChildren() {
			return children;
		}

		public bool IsPlayerSpeaking(){
			return isPlayerSpeaking;
		}

        public string GetOnEnterAction(){
            return onEnterAction;
        }

        public string GetOnExitAction(){
            return onExitAction;
        }

#if UNITY_EDITOR

		public void SetPosition(Vector2 newPosition){
			Undo.RecordObject(this, "Move Dialogue Node");
			rect.position = newPosition;
			EditorUtility.SetDirty(this);
		}

		public void SetText(string newText){
			if(newText != text){
				Undo.RecordObject(this, "Update Dialogue Text");
				text = newText;
				EditorUtility.SetDirty(this);
			}
		}

		public void AddChild(string childID){
			Undo.RecordObject(this, "Add Dialogue Link");
			children.Add(childID);
			EditorUtility.SetDirty(this);
		}

		public void RemoveChild(string childID){
			Undo.RecordObject(this, "Remove Dialogue Link");
			children.Remove(childID);
			EditorUtility.SetDirty(this);
		}

		public void SetPlayerSpeaking(bool newIsPlayerSpeaking){
			Undo.RecordObject(this, "Change Dialogue Speaker");
			isPlayerSpeaking = newIsPlayerSpeaking;
			EditorUtility.SetDirty(this);
		}
#endif
	}
}

