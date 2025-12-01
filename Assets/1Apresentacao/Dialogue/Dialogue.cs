using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;

namespace DialogueSystem {

    [CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue", order = 0)]
    public class Dialogue : ScriptableObject, ISerializationCallbackReceiver {

        [SerializeField] private List<DialogueNode> nodes = new List<DialogueNode>();
        [SerializeField] private Vector2 newNodeOffset = new Vector2(250, 0);

        private Dictionary<string, DialogueNode> nodeLookup = new Dictionary<string, DialogueNode>();

        #region Node Lookup
        private void OnValidate() {
            nodeLookup.Clear();
            foreach (DialogueNode node in nodes) {
                if (node != null && !string.IsNullOrEmpty(node.name)) {
                    nodeLookup[node.name] = node;
                }
            }
        }

        public IEnumerable<DialogueNode> GetAllNodes() => nodes;

        public DialogueNode GetRootNode() {
            if (nodes.Count == 0) return null;
            return nodes[0];
        }

        public IEnumerable<DialogueNode> GetAllChildren(DialogueNode parentNode) {
            foreach (string childID in parentNode.GetChildren()) {
                if (nodeLookup.ContainsKey(childID))
                    yield return nodeLookup[childID];
            }
        }

        public IEnumerable<DialogueNode> GetPlayerChildren (DialogueNode currentNode) {
            foreach(DialogueNode node in GetAllChildren(currentNode)) {
                if(node.IsPlayerSpeaking()){
                    yield return node;
                }
            }
        }

        public IEnumerable<DialogueNode> GetAIChildren (DialogueNode currentNode) {
            foreach(DialogueNode node in GetAllChildren(currentNode)) {
                if(!node.IsPlayerSpeaking()){
                    yield return node;
                }
            }
        }

        #endregion

#if UNITY_EDITOR
        private void OnEnable() {
            // Ensure root node exists if the asset is empty
            if (nodes.Count == 0) {
                CreateRootNode();
            }
        }

        private void CreateRootNode() {
            DialogueNode rootNode = CreateInstance<DialogueNode>();
            rootNode.name = Guid.NewGuid().ToString();
            nodes.Add(rootNode);

            // Add to asset
            if (AssetDatabase.GetAssetPath(this) != "") {
                AssetDatabase.AddObjectToAsset(rootNode, this);
                AssetDatabase.SaveAssets();
                EditorUtility.SetDirty(this);
            }
        }

        public void CreateNode(DialogueNode parent) {
            DialogueNode newNode = MakeNode(parent);

            Undo.RegisterCreatedObjectUndo(newNode, "Created Dialogue Node");
            Undo.RecordObject(this, "Added Dialogue Node");

            AddNode(newNode);
        }

        public void DeleteNode(DialogueNode nodeToDelete) {
            if (!nodes.Contains(nodeToDelete)) return;

            Undo.RecordObject(this, "Deleted Dialogue Node");

            nodes.Remove(nodeToDelete);
            CleanDanglingChildren(nodeToDelete);

            Undo.DestroyObjectImmediate(nodeToDelete);
            OnValidate();

            // Ensure at least one node exists
            if (nodes.Count == 0) {
                CreateRootNode();
            }
        }

        private DialogueNode MakeNode(DialogueNode parent) {
            DialogueNode newNode = CreateInstance<DialogueNode>();
            newNode.name = Guid.NewGuid().ToString();

            if (parent != null) {
                parent.AddChild(newNode.name);
                newNode.SetPlayerSpeaking(!parent.IsPlayerSpeaking());
                newNode.SetPosition(parent.GetRect().position + newNodeOffset);
            }

            // Attach to asset
            if (AssetDatabase.GetAssetPath(this) != "") {
                AssetDatabase.AddObjectToAsset(newNode, this);
                EditorUtility.SetDirty(this);
            }

            return newNode;
        }

        private void AddNode(DialogueNode node) {
            nodes.Add(node);
            OnValidate();
        }

        private void CleanDanglingChildren(DialogueNode nodeToDelete) {
            foreach (DialogueNode node in nodes) {
                node.RemoveChild(nodeToDelete.name);
            }
        }
#endif

        #region Serialization Callbacks
        public void OnBeforeSerialize() { }

        public void OnAfterDeserialize() { }
        #endregion
    }
}
