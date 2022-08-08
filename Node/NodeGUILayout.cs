using UnityEditor;
using UnityEngine;

namespace Irehon.Editor
{
    public class NodeGUILayout
    {
        private NodeGUIStyle style;
        private AssetNode rootNode;
        
        public NodeGUILayout(AssetNode rootNode, NodeGUIStyle style = null) //TODO: layout preferences : show toggle, openable, const value
        {
            this.rootNode = rootNode;
            
            if (style == null)
                style = new NodeGUIStyle();
            
            this.style = style;
        }

        public void LayoutRootNode()
        {
            LayoutNode(rootNode, 0);
        }
        
        public void LayoutChildsNode()
        {
            foreach (AssetNode node in rootNode.GetChilds())
                LayoutNode(node, 0);
        }
        
        private void LayoutNode(AssetNode node, int depth)
        {
            if (node.IsEndNode())
                LayoutFileNodeHorizontalTab(node, depth);
            else
                LayoutFolderNode(node, depth);
        }
        
        private void LayoutFolderNode(AssetNode node, int depth)
        {
            LayoutFolderNodeHorizontalTab(node, depth);
                
            if (node.IsOpen)
            {
                foreach (AssetNode childNode in node.GetChilds())
                    LayoutNode(childNode, depth + 1);
            }
        }

        private void LayoutFileNodeHorizontalTab(AssetNode node, int depth)
        {
            EditorGUILayout.BeginHorizontal();
                
            if (style.renderToggle)
                node.SetActive(EditorGUILayout.Toggle(node.IsActive(), GUILayout.Width(style.toggleWidth)));
                
            if (depth != 0)
                GUILayout.Space(style.depthPixelsOffset * depth + style.objectPixelsOffset);
                
            EditorGUILayout.ObjectField(node.GetAsset(), typeof(Object), GUILayout.Width(style.objectFieldWidth));
                
            EditorGUILayout.EndHorizontal();
        }

        private void LayoutFolderNodeHorizontalTab(AssetNode node, int depth)
        {
            EditorGUILayout.BeginHorizontal();
                
            if (style.renderToggle)
                node.SetActive(EditorGUILayout.Toggle(node.IsActive(), GUILayout.Width(style.toggleWidth)));
                
            if (depth != 0)
                GUILayout.Space(depth * style.depthPixelsOffset);
            node.IsOpen = EditorGUILayout.Foldout(node.IsOpen, node.GetData());
                
            EditorGUILayout.EndHorizontal();
        }
    }
}