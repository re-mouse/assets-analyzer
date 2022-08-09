using System.Linq;
using UnityEditor;

namespace Irehon.Editor
{
    public class ProjectEditorUtilities
    {
        [MenuItem("Project/Show Assets Dependency")]
        private static void ShowAssetsDependenciesWindow()
        {
            NodeViewerWindow.CreateAndShow();
        }

        [MenuItem("Project/Show Unused Assets")]
        private static void ShowUnusedAssetsWindow()
        {
            AssetNodeCleanerWindow.CreateAndShow();
        }

        public static string[] GetUnusedPaths()
        {
            string[] allAssetsPaths = AssetDatabase.GetAllAssetPaths();
            string[] dependenciesPaths = GetDependenciesPath();

            return allAssetsPaths.Except(dependenciesPaths).ToArray();
        }
        
        public static string[] GetDependenciesPath()
        {
            var gameScenes = AssetDatabase.FindAssets("t:Scene", new string[] { "Assets/Scenes" });
            
            string[] scenesAssetId = new string[gameScenes.Length];

            for (int i = 0; i < gameScenes.Length; i++)
                scenesAssetId[i] = AssetDatabase.GUIDToAssetPath(gameScenes[i]);;

            return AssetDatabase.GetDependencies(scenesAssetId);
        }
    }
}