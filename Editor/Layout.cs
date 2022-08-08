using System;
using System.Collections.Generic;
using UnityEngine;

namespace DependenciesHunter
{
    public static class Layout {
        public static void Horizontal(Action block, int width = 0, int height = 0)
        {
            List<GUILayoutOption> options = new List<GUILayoutOption>();
            if (width != 0)
                options.Add(GUILayout.);
            GUILayout.BeginHorizontal();
            
            block();
            
            GUILayout.EndHorizontal();
        }

        public static void Vertical(Action block) {
            GUILayout.BeginVertical();
            
            block();
            
            GUILayout.EndVertical();
        }
    }
}