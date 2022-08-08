using System.Text;
using UnityEditor;
using UnityEngine;

namespace Irehon.Editor
{
    public class PathNode : Node<string>
    {
        public bool IsOpen;
        private bool isActive;

        public PathNode(string name, int depth) : base(name, depth) { }
        
        public void SetActive(bool isActive)
        {
            if (this.isActive != isActive)
            {
                UpdateActiveOnChilds(isActive);
            }

            this.isActive = isActive;
        }

        public bool IsActive()
        {
            return isActive;
        }

        public string GetRelativePath()
        {
            StringBuilder path = new StringBuilder();
            
            Node<string> currentNode = this;
            
            int i = 0;
            
            while (!currentNode.IsRootNode())
            {
                string name = currentNode.GetData();
                
                if (i != 0)
                    name += "/";
                
                path.Insert(0, name);
                currentNode = currentNode.parent;
                i++;
            }

            return path.ToString();
        }

        private void UpdateActiveOnChilds(bool isActive)
        {
            foreach (PathNode node in childs)
                node.SetActive(isActive);
        }
    }
}