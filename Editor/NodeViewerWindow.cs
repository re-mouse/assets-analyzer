using System;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Irehon.Editor
{
    public class NodeViewerWindow : EditorWindow
    {
        private static readonly string WindowName = "Assets dependencies";
        private NodeGUILayout nodeGUILayout;
        private Vector2 scrollPosition;

        public static void CreateAndShow()
        {
            var window = (NodeViewerWindow)GetWindow(typeof(NodeViewerWindow), true, WindowName);
            
            window.BuildNodeLayout();
            
            window.Show();
        }
        
        private void BuildNodeLayout()
        {
            nodeGUILayout = new NodeGUILayout(GetDependencyNode(), false);
        }

        private AssetNode GetDependencyNode()
        {
            AssetNodeBuilder dependenciesNodeBuilder = new AssetNodeBuilder();

            return dependenciesNodeBuilder.GetAssetNodes(ProjectEditorUtilities.GetDependenciesPath());
        }

        private void OnGUI()
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition,false,true, GUILayout.ExpandHeight(true));
 
            nodeGUILayout.LayoutRootNodeContent();
 
            GUILayout.EndScrollView ();
        }
    }
}