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
            LayoutNode(rootNode);
        }
        
        public void LayoutChildsNode()
        {
            foreach (AssetNode node in rootNode.GetChilds())
                LayoutNode(node);
        }
        
        private void LayoutNode(AssetNode node)
        {
            if (node.IsEndNode())
                LayoutFileNodeHorizontalTab(node);
            else
                LayoutFolderNode(node);
        }
        
        private void LayoutFolderNode(AssetNode node)
        {
            LayoutFolderNodeHorizontalTab(node);
                
            if (node.IsOpen)
            {
                foreach (AssetNode childNode in node.GetChilds())
                    LayoutNode(childNode);
            }
        }

        private void LayoutFileNodeHorizontalTab(AssetNode node)
        {
            EditorGUILayout.BeginHorizontal(GUILayout.Width(style.objectFieldWidth + style.fileSizeBlockWidth));

            {
                EditorGUILayout.BeginHorizontal(GUILayout.Width(style.objectFieldWidth));

                if (style.renderToggle)
                    node.SetActive(EditorGUILayout.Toggle(node.IsActive(), GUILayout.Width(style.toggleWidth)));

                if (node.depth != 0)
                    GUILayout.Space(style.depthPixelsOffset * node.depth + style.objectPixelsOffset);

                EditorGUILayout.ObjectField(node.GetAsset(), typeof(Object), GUILayout.Width(style.objectFieldWidth));

                EditorGUILayout.EndHorizontal();
            }

            {
                EditorGUILayout.BeginHorizontal(GUILayout.Width(style.fileSizeBlockWidth));

                EditorGUILayout.LabelField(node.GetSizeBytes().ToString());

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndHorizontal();
        }

        private void LayoutFolderNodeHorizontalTab(AssetNode node)
        {
            EditorGUILayout.BeginHorizontal(GUILayout.Width(style.objectFieldWidth));
                
            if (style.renderToggle)
                node.SetActive(EditorGUILayout.Toggle(node.IsActive(), GUILayout.Width(style.toggleWidth)));
                
            if (node.depth != 0)
                GUILayout.Space(node.depth * style.depthPixelsOffset);
            
            node.IsOpen = EditorGUILayout.Foldout(node.IsOpen, node.GetData());
                
            EditorGUILayout.EndHorizontal();
        }
    }
}