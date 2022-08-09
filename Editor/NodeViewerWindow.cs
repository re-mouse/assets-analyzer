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
        private NodeGUILayout nodeGUILayout;
        private Vector2 scrollPosition;

        public static NodeViewerWindow CreateWindow(AssetNode node, string windowName)
        {
            var window = (NodeViewerWindow)GetWindow(typeof(NodeViewerWindow), true, windowName);
            
            window.Setup(node);
            
            return window;
        }

        public void Setup(AssetNode baseNode)
        {
            nodeGUILayout = new NodeGUILayout(baseNode);
        }

        private void OnGUI()
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition,false,true, GUILayout.ExpandHeight(true));
 
            nodeGUILayout.LayoutRootNodeContent();
 
            GUILayout.EndScrollView ();
        }
    }
}