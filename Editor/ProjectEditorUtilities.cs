using System.Linq;
using UnityEditor;

namespace Irehon.Editor
{
    public class ProjectEditorUtilities
    {
        [MenuItem("Project/Show Assets Dependency")]
        private static void ShowAssetsDependenciesWindow()
        {
            NodeViewerWindow.CreateAndShow(GetDependenciesPath());
        }
        
        [MenuItem("Project/Show All Assets")]
        private static void ShowAllAssetsWindow()
        {
            NodeViewerWindow.CreateAndShow(GetAllAssetsPaths());
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

        public static string[] GetAllAssetsPaths()
        {
            return AssetDatabase.GetAllAssetPaths();
        }
        
        public static string[] GetDependenciesPath()
        {
            var scenes = EditorBuildSettings.scenes;
            string[] gameScenesPath = new string[scenes.Length];

            for (int i = 0; i < scenes.Length; i++)
                gameScenesPath[i] = scenes[i].path;

            return AssetDatabase.GetDependencies(gameScenesPath);
        }
    }
}