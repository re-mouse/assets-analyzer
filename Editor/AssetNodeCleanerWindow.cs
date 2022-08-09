using System.IO;
using UnityEditor;
using UnityEngine;

namespace Irehon.Editor
{
    public class AssetNodeCleanerWindow : EditorWindow
    {
        private static readonly string WindowName = "Unused assets";
        
        private NodeGUILayout nodeGUILayout;
        private Vector2 scrollPosition;
        private AssetNode rootNode;

        public static void CreateAndShow()
        {
            var window = (AssetNodeCleanerWindow)GetWindow(typeof(AssetNodeCleanerWindow), true, WindowName);
            
            window.BuildNodeLayout();
            
            window.Show();
        }

        private void BuildNodeLayout()
        {
            BuildRootNode();
            
            nodeGUILayout = new NodeGUILayout(rootNode, true);
        }

        private void BuildRootNode()
        {
            AssetNodeBuilder dependenciesNodeBuilder = new AssetNodeBuilder();

            rootNode = dependenciesNodeBuilder.GetFilteredAssetNodes(ProjectEditorUtilities.GetUnusedPaths(), IsAcceptableAssetPath);
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
            {
                DeleteActiveAssetNodes(rootNode);
                BuildNodeLayout();
            }
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
            string path = node.GetRelativePath();
            path = path.Insert(0, "Assets/");
            AssetDatabase.DeleteAsset(path);
        }
        
        private static bool IsAcceptableAssetPath(string path)
        {
            if (Directory.Exists(path))
                return false;
            
            return true;
        }
    }
}