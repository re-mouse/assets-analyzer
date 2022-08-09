using System;
using UnityEditor;
using UnityEngine;

namespace Irehon.Editor
{
    public static class NodeGUIPreferences
    {
        public static readonly int FileSizeBlockWidth = 20;
        public static readonly int FileBlockWidth = 300;
        
        public static readonly int AssetFieldWidth = 150;
        public static readonly int ToggleWidth = 7;
        public static readonly int NodeUsageWidth = 100;
        
        public static readonly int DepthPixelsOffset = 25;
        public static readonly int AssetPixelsOffset = 15;
    }
    
    public class NodeGUILayout
    {
        private AssetNode targetedNode;
        private bool isRenderToggle;
        private AssetNode rootNode;
        
        public NodeGUILayout(AssetNode rootNode, bool isRenderToggle = false)
        {
            this.rootNode = rootNode;

            this.isRenderToggle = isRenderToggle;
        }
        
        public void LayoutRootNodeContent()
        {
            LayoutNodeExpandButtons();
            
            EditorGUILayout.BeginVertical();
            
            LayoutNode(rootNode);

            EditorGUILayout.EndVertical();
        }

        private void LayoutNodeExpandButtons()
        {
            EditorGUILayout.BeginHorizontal();
            
            if (GUILayout.Button("Expand all (Cause lags)"))
                rootNode.SetOpenAllNodes(true);
            
            if (GUILayout.Button("Hide all"))
                rootNode.SetOpenAllNodes(false);

            EditorGUILayout.EndHorizontal();
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
            
            LayoutRowColumn(NodeGUIPreferences.NodeUsageWidth, options => LayoutNodePercentUsage(node));

            EditorGUILayout.EndHorizontal();
            
            DrawHorizontalLine(Color.black);
        }

        private void LayoutRowColumn(int width, Action<GUILayoutOption[]> columnBlock, params GUILayoutOption[] options)
        {
            GUILayout.BeginHorizontal(GUILayout.Width(width));
            columnBlock(options);
            GUILayout.EndHorizontal();
        }

        private void LayoutNodePercentUsage(AssetNode node)
        {
            float usagePercentFromRoot = (float)node.size / rootNode.size;

            int usageWidth = Mathf.RoundToInt(NodeGUIPreferences.NodeUsageWidth * usagePercentFromRoot);
            int freeWidth = NodeGUIPreferences.NodeUsageWidth - usageWidth;
            
            Rect rect = EditorGUILayout.GetControlRect(GUILayout.Width(usageWidth));
            EditorGUI.DrawRect(rect, Color.blue);
            
            rect = EditorGUILayout.GetControlRect(GUILayout.Width(freeWidth));
            EditorGUI.DrawRect(rect, Color.white);
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