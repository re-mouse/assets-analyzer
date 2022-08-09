using System;
using UnityEditor;
using UnityEngine;

namespace Irehon.Editor
{
    public static class NodeGUIPreferences
    {
        public static readonly int FileSizeBlockWidth = 50;
        public static readonly int FileBlockWidth = 350;
        
        public static readonly int AssetFieldWidth = 100;
        public static readonly int ToggleWidth = 7;
        
        public static readonly int DepthPixelsOffset = 16;
        public static readonly int AssetPixelsOffset = 15;
    }
    
    public class NodeGUILayout
    {
        private AssetNode targetedNode;
        private bool isRenderToggle;
        private AssetNode rootNode;
        
        public NodeGUILayout(AssetNode rootNode, bool isRenderToggle = false) //TODO: layout preferences : show toggle, openable, const value
        {
            this.rootNode = rootNode;

            this.isRenderToggle = isRenderToggle;
        }
        
        public void LayoutRootNodeContent()
        {
            EditorGUILayout.BeginVertical();
            
            foreach (AssetNode child in rootNode.GetChilds())
                LayoutNode(child);

            EditorGUILayout.EndVertical();
        }

        private void LayoutNode(AssetNode node)
        {
            LayoutNodeRow(node);
            
            if (node.IsOpen)
            {
                foreach (AssetNode child in node.GetChilds())
                    LayoutNode(child);
            }
        }

        private void LayoutNodeRow(AssetNode node)
        {
            EditorGUILayout.BeginHorizontal();
            
            if (isRenderToggle)
                LayoutRowColumn(NodeGUIPreferences.ToggleWidth, node.LayoutToggle);
            
            LayoutRowColumn(NodeGUIPreferences.FileBlockWidth, node.LayoutAssetWithOffset);

            LayoutRowColumn(NodeGUIPreferences.FileSizeBlockWidth, node.LayoutFileSize);

            EditorGUILayout.EndHorizontal();
            
            DrawHorizontalLine(Color.black);
        }

        private void LayoutRowColumn(int width, Action<GUILayoutOption[]> columnBlock, params GUILayoutOption[] options)
        {
            GUILayout.BeginHorizontal(GUILayout.Width(width));
            columnBlock(options);
            GUILayout.EndHorizontal();
        }
        
        private GUIStyle GetStyleByColor(Color color)
        {
            GUIStyle style = new GUIStyle();
            Texture2D texture = new Texture2D(1, 1);
            
            texture.SetPixel(0, 0, color);
            texture.Apply();
            style.normal.background = texture;
            return style;
        }
        
        private void DrawHorizontalLine(Color color, int thickness = 1, int padding = 2)
        {
            Rect rect = EditorGUILayout.GetControlRect(GUILayout.Height(padding + thickness));
            
            rect.height = thickness;
            rect.y += padding / 2;
            rect.x -= 2;
            rect.width += 6;
            
            EditorGUI.DrawRect(rect, color);
        }
    }
}