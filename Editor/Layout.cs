using System;
using System.Collections.Generic;
using UnityEngine;

namespace Irehon.Editor
{
    public static class Layout {
        public static void Horizontal(Action block, int width = 0, int height = 0)
        {
            GUILayout.BeginHorizontal(GetOptions(width, height));
            
            block();
            
            GUILayout.EndHorizontal();
        }

        public static void Vertical(Action block, int width, int height) {
            GUILayout.BeginVertical(GetOptions(width, height));
            
            block();
            
            GUILayout.EndVertical();
        }

        public static GUILayoutOption[] GetOptions(int width, int height)
        {
            List<GUILayoutOption> options = new List<GUILayoutOption>();
            if (width > 0)
                options.Add(GUILayout.Width(width));
            if (height > 0)
                options.Add(GUILayout.Height(height));
            return options.ToArray();
        }
    }
}