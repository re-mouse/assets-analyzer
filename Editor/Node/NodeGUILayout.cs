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
            LayoutNodePath(rootNode);
        }
        
        public void LayoutChildsNode()
        {
            EditorGUILayout.BeginHorizontal();
            
            EditorGUILayout.BeginVertical(GUILayout.Width(style.fileSizeBlockWidth));
            foreach (AssetNode node in rootNode.GetChilds())
                LayoutNodeFileSize(node);
            EditorGUILayout.EndVertical();
            
            EditorGUILayout.BeginVertical();
            foreach (AssetNode node in rootNode.GetChilds())
                LayoutNodePath(node);
            EditorGUILayout.EndVertical();
            
            
            EditorGUILayout.EndHorizontal();
        }

        private void LayoutNodeFileSize(AssetNode node)
        {
            EditorGUILayout.LabelField(node.GetReadableTotalSize(), GUILayout.Height(style.rowHeight));
            if (node.IsOpen)
            {
                foreach (AssetNode child in node.GetChilds())
                {
                    if (child.IsOpen || child.IsEndNode())
                        LayoutNodeFileSize(child);
                }
            }
        }
        
        private void LayoutNodePath(AssetNode node)
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
                    LayoutNodePath(childNode);
            }
        }

        private void LayoutFileNodeHorizontalTab(AssetNode node)
        {
            EditorGUILayout.BeginHorizontal(GUILayout.Width(style.objectFieldWidth), GUILayout.Height(style.rowHeight));

            if (style.renderToggle)
                node.SetActive(EditorGUILayout.Toggle(node.IsActive(), GUILayout.Width(style.toggleWidth)));

            if (node.depth != 0)
                GUILayout.Space(style.depthPixelsOffset * node.depth + style.objectPixelsOffset);

            EditorGUILayout.ObjectField(node.GetAsset(), typeof(Object), GUILayout.Width(style.objectFieldWidth));

            EditorGUILayout.EndHorizontal();
        }

        private void LayoutFolderNodeHorizontalTab(AssetNode node)
        {
            EditorGUILayout.BeginHorizontal(GUILayout.Width(style.objectFieldWidth), GUILayout.Height(style.rowHeight));
                
            if (style.renderToggle)
                node.SetActive(EditorGUILayout.Toggle(node.IsActive(), GUILayout.Width(style.toggleWidth)));
                
            if (node.depth != 0)
                GUILayout.Space(node.depth * style.depthPixelsOffset);
            
            node.IsOpen = EditorGUILayout.Foldout(node.IsOpen, node.GetData());
                
            EditorGUILayout.EndHorizontal();
        }
    }
}