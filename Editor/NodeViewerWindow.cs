using UnityEditor;
using UnityEngine;

namespace Irehon.Editor
{
    public class NodeViewerWindow : EditorWindow
    {
        private static readonly string WindowName = "Assets dependencies";
        private NodeGUILayout nodeGUILayout;
        private Vector2 scrollPosition;

        public static void CreateAndShow(string[] paths)
        {
            var window = (NodeViewerWindow)GetWindow(typeof(NodeViewerWindow), true, WindowName);
            
            window.BuildNodeLayout(paths);
            
            window.Show();
        }
        
        private void BuildNodeLayout(string[] paths)
        {
            nodeGUILayout = new NodeGUILayout(GetDependencyNode(paths), false);
        }

        private AssetNode GetDependencyNode(string[] paths)
        {
            AssetNodeBuilder dependenciesNodeBuilder = new AssetNodeBuilder();

            return dependenciesNodeBuilder.GetAssetNodes(paths);
        }

        private void OnGUI()
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition,false,true, GUILayout.ExpandHeight(true));
 
            nodeGUILayout.LayoutRootNodeContent();
 
            GUILayout.EndScrollView ();
        }
    }
}