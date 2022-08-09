using UnityEditor;
using UnityEngine;

namespace Irehon.Editor
{
    public class UnusedAssetsCleanerWindow : EditorWindow
    {
        private NodeGUILayout nodeGUILayout;
        private Vector2 scrollPosition;

        public static UnusedAssetsCleanerWindow CreateWindow(AssetNode node, string windowName)
        {
            var window = (UnusedAssetsCleanerWindow)GetWindow(typeof(UnusedAssetsCleanerWindow), true, windowName);
            
            window.Setup(node);
            
            return window;
        }

        public void Setup(AssetNode baseNode)
        {
            nodeGUILayout = new NodeGUILayout(baseNode);
        }

        private void OnGUI()
        {
            GUILayout.BeginVertical();
 
            scrollPosition = GUILayout.BeginScrollView(scrollPosition,false,true, GUILayout.ExpandHeight(true));
 
            nodeGUILayout.LayoutRootNodeContent();
 
            GUILayout.EndScrollView ();
            GUILayout.EndVertical();
        }
    }
}