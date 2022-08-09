using System;
using UnityEditor;
using UnityEngine;

namespace Irehon.Editor
{
    public static class NodeGUIPreferences
    {
        public static readonly int WindowHeight = 300;
        public static readonly int FileBlockWidth = 350;
        public static readonly int RowHeight = 40;
        public static readonly int FileSizeBlockWidth = 50;
        public static readonly int DepthPixelsOffset = 16;
        public static readonly int AssetPixelsOffset = 15;
        public static readonly int ToggleWidth = 20;
        public static readonly int ObjectFieldWidth = 400;
    }
    
    public class NodeGUILayout
    {
        private bool isRenderToggle;
        private AssetNode rootNode;
        private int rowCount;
        
        public NodeGUILayout(AssetNode rootNode, bool isRenderToggle = true) //TODO: layout preferences : show toggle, openable, const value
        {
            this.rootNode = rootNode;

            this.isRenderToggle = isRenderToggle;
        }
        
        public void LayoutChildsNode()
        {
            EditorGUILayout.BeginVertical();
            
            foreach (AssetNode child in rootNode.GetChilds())
                LayoutNodeRow(child);

            EditorGUILayout.EndVertical();
        }

        private void LayoutNodeRow(AssetNode node)
        {
            EditorGUILayout.BeginHorizontal();

            if (isRenderToggle)
                LayoutRowColumn(NodeGUIPreferences.ToggleWidth, node.LayoutToggle);
            
            LayoutRowColumn(NodeGUIPreferences.FileBlockWidth, node.LayoutAssetWithOffset);

            LayoutRowColumn(NodeGUIPreferences.FileSizeBlockWidth, node.LayoutFileSize);

            EditorGUILayout.EndHorizontal();
        }

        private void LayoutRowColumn(int width, Action<GUILayoutOption[]> columnBlock, params GUILayoutOption[] options)
        {
            GUILayout.BeginHorizontal(GUILayout.Width(width));
            columnBlock(options);
            GUILayout.EndHorizontal();
        }
    }
}