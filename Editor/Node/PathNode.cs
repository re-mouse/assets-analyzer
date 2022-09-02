using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace Irehon.Editor
{
    public class PathNode : Node<string>
    {
        public bool IsOpen;
        public bool IsFolder { get => (GetAttributes() & FileAttributes.Directory) != 0; }
        
        private bool isActive;
        private FileAttributes cachedAttributes;
        private FileInfo cachedFileInfo;

        public PathNode(string name, int depth) : base(name, depth) { }

        public void SetOpenAllNodes(bool isOpen)
        {
            IsOpen = isOpen;
            foreach (PathNode child in childs)
                child.SetOpenAllNodes(isOpen);
        }

        public void DisableParentNodes(PathNode node)
        {
            node.isActive = false;
            if (node.parent != null)
                node.DisableParentNodes((PathNode)node.parent);
        }

        public FileAttributes GetAttributes()
        {
            if (cachedAttributes == 0)
                CacheAttributes();
            return cachedAttributes;
        }
        
        public FileInfo GetPathFileInfo()
        {
            if (IsFolder)
                throw new Exception("Not a file");
            
            if (cachedFileInfo == null)
                CacheFileInfo();

            return cachedFileInfo;
        }
        
        public void SetActive(bool isActive)
        {
            if (this.isActive != isActive)
            {
                UpdateActiveOnChilds(isActive);
                if (!isActive)
                    DisableParentNodes((PathNode)parent);
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
        
        public string GetFullPath()
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
        
        public void CacheFileInfo()
        {
            cachedFileInfo = new FileInfo(GetFullPath());
        }

        public void CacheAttributes()
        {
            cachedAttributes = File.GetAttributes(GetFullPath());
        }

        private void UpdateActiveOnChilds(bool isActive)
        {
            foreach (PathNode node in childs)
                node.SetActive(isActive);
        }
    }
}