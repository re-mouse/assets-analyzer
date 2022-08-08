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

        public AssetNode GetAssetNodes(string[] paths, bool activeNodes = true, bool openNodes = false)
        {
            AssetNode baseNode = new AssetNode(rootName, 0);
            baseNode.IsOpen = openNodes;
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
                        AssetNode newNode = new AssetNode(pathObjects[i], i);
                        newNode.IsOpen = openNodes;
                        currentNode.InsertNode(newNode);
                        currentNode = newNode;
                    }
                    else
                        currentNode = findingNode;
                }
            }
            
            baseNode.SetActive(activeNodes);
            baseNode.SortByChildCountAllNodes();

            return baseNode;
        }

        private bool IsAssetsFolder(string[] pathObjects) =>
            pathObjects[0] == "Assets";
    }
}