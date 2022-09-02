using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Irehon.Editor
{
    public static class AssetNodeLayoutExtension {

        public static void LayoutFileSize(this AssetNode node, params GUILayoutOption[] options)
        {
            options = options.AddOption(GUILayout.Width(NodeGUIPreferences.FileBlockWidth));
            EditorGUILayout.LabelField(node.GetReadableTotalSize(), options);
        }

        public static void LayoutAssetWithOffset(this AssetNode node, params GUILayoutOption[] options)
        {
            if (node.depth != 0)
                GUILayout.Space(node.GetOffset());
            
            if (node.IsEndNode() && !node.IsFolder)
                node.LayoutAsAsset(options);
            else
                node.LayoutAsFolder(options);
        }

        public static void LayoutToggle(this AssetNode node, params GUILayoutOption[] options)
        {
            node.SetActive(EditorGUILayout.Toggle(node.IsActive(), options));
        }

        private static int GetOffset(this AssetNode node)
        {
            int offset = NodeGUIPreferences.DepthPixelsOffset * node.depth;

            if (node.IsEndNode())
                offset += NodeGUIPreferences.AssetPixelsOffset;

            return offset;
        }
        
        private static void LayoutAsFolder(this AssetNode node, params GUILayoutOption[] options)
        {
            node.IsOpen = EditorGUILayout.Foldout(node.IsOpen, node.GetData());
        }

        private static void LayoutAsAsset(this AssetNode node, params GUILayoutOption[] options)
        {
            options = options.AddOption(GUILayout.Width(NodeGUIPreferences.AssetFieldWidth));
            
            EditorGUILayout.ObjectField(node.GetAsset(), typeof(Object), options);
        }

        private static GUILayoutOption[] AddOption(this GUILayoutOption[] options, GUILayoutOption option)
        {
            List<GUILayoutOption> newOptions = new List<GUILayoutOption>(options);
            newOptions.Add(GUILayout.Width(NodeGUIPreferences.AssetFieldWidth));
            return newOptions.ToArray();
        }
    }
}