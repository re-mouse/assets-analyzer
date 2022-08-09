using UnityEditor;
using UnityEngine;

namespace Irehon.Editor
{
    public class AssetNodeCleanerWindow : EditorWindow
    {
        private NodeGUILayout nodeGUILayout;
        private Vector2 scrollPosition;
        private AssetNode rootNode;

        public static AssetNodeCleanerWindow CreateWindow(AssetNode node, string windowName)
        {
            var window = (AssetNodeCleanerWindow)GetWindow(typeof(AssetNodeCleanerWindow), true, windowName);
            
            window.Setup(node);
            
            return window;
        }

        public void Setup(AssetNode rootNode)
        {
            this.rootNode = rootNode;
            nodeGUILayout = new NodeGUILayout(this.rootNode, true);
        }

        private void OnGUI()
        {
            LayoutNodeBlock();
            LayoutDeleteButtons();
        }

        private void LayoutNodeBlock()
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true, GUILayout.ExpandHeight(true));
 
            nodeGUILayout.LayoutRootNodeContent();
 
            GUILayout.EndScrollView ();
        }

        private void LayoutDeleteButtons()
        {
            if (GUILayout.Button("Delete selected"))
                DeleteActiveAssetNodes(rootNode);
        }

        private void DeleteActiveAssetNodes(AssetNode node)
        {
            if (node.IsEndNode() && node.IsActive())
                DeleteAssetNode(node);
            
            foreach (AssetNode child in node.GetChilds())
            {
                DeleteActiveAssetNodes(child);
            }
        }

        private void DeleteAssetNode(AssetNode node)
        {
            AssetDatabase.DeleteAsset(node.data);
        }
    }
}