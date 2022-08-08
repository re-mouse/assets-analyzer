using System.Text;
using UnityEditor;
using UnityEngine;

namespace Irehon.Editor
{
    public class AssetNode : PathNode
    {
        public AssetNode(string name, int depth) : base(name, depth) { }
        
        public Object GetAsset()
        {
            if (!IsEndNode())
                return null;
            
            return EditorUtility.FindAsset(GetRelativePath(), typeof(Object));
        }

        public long GetSizeBytes()
        {
            return new System.IO.FileInfo(GetFullPath()).Length;
        }
        
        private string GetFullPath()
        {
            StringBuilder path = new StringBuilder();
            
            Node<string> currentNode = this;
            
            int i = 0;

            if (currentNode.parent != null && !currentNode.parent.IsRootNode())
            {
                while (currentNode != null && currentNode.parent != null)
                {
                    string name = currentNode.GetData();

                    if (i != 0)
                        name += "/";

                    path.Insert(0, name);
                    currentNode = currentNode.parent;
                    i++;
                }
            }
            else
                path.Insert(0, currentNode.GetData());

            path.Insert(0, Application.dataPath + "/");

            return path.ToString();
        }
    }
}