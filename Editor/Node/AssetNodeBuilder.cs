using System;

namespace Irehon.Editor
{
    public class AssetNodeBuilder
    {
        private string rootName;
        
        public AssetNodeBuilder(string rootName = "Assets")
        {
            if (rootName == null)
                throw new NullReferenceException();
            this.rootName = rootName;
        }

        public AssetNode GetAssetNodes(string[] paths)
        {
            AssetNode baseNode = new AssetNode(rootName, 0, true);
            FillNodeWithPaths(baseNode, paths);

            baseNode.ClearFoldersOnChilds();

            baseNode.SetActive(false);
            baseNode.CalculateTotalNodeSize();
            baseNode.SortAllNodes();

            return baseNode;
        }
        
        public AssetNode GetFilteredAssetNodes(string[] paths, Func<string, bool> filteringAssetsPattern)
        {
            AssetNode baseNode = new AssetNode(rootName, 0, true);
            FillNodeWithPaths(baseNode, paths);
            
            baseNode.FilterEndNodes(filteringAssetsPattern);

            baseNode.ClearFoldersOnChilds();

            baseNode.SetActive(true);
            baseNode.CalculateTotalNodeSize();
            baseNode.SortAllNodes();

            return baseNode;
        }

        private static void FillNodeWithPaths(AssetNode baseNode, string[] paths)
        {
            foreach (string path in paths)
            {
                string[] pathObjects = path.Split('/');
                
                if (!IsAssetsFolder(pathObjects))
                    continue;

                int objectLength = pathObjects.Length;

                AssetNode currentNode = baseNode;
                for (int i = 1; i < objectLength; i++)
                {
                    AssetNode findingNode = (AssetNode)currentNode.FindNode(pathObjects[i]);
                    if (findingNode == null)
                    {
                        bool isFolder = i != objectLength - 1;
                        AssetNode newNode = new AssetNode(pathObjects[i], i, isFolder);
                        currentNode.InsertNode(newNode);
                        currentNode = newNode;
                    }
                    else
                        currentNode = findingNode;
                }
            }
        }

        private static bool IsAssetsFolder(string[] pathObjects) =>
            pathObjects[0] == "Assets";
    }
}