using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Irehon.Editor
{
    public class AssetNode : PathNode
    {
        public AssetNode(string name, int depth, bool isFolder) : base(name, depth)
        {
            this.isFolder = isFolder;
        }

        public long size { get; private set; }
        
        private bool isFolder;
        private string cachedReadableSize;
        private Object asset;
        
        public Object GetAsset()
        {
            if (asset == null)
                CacheAsset();
            return asset;
        }

        public string GetReadableTotalSize()
        {
            return cachedReadableSize;
        }

        public void Remove(AssetNode node)
        {
            if (!childs.Contains(node))
                throw new ArgumentException();

            childs.Remove(node);
            if (childs.Count == 0)
                ((AssetNode)parent).Remove(this);
        }

        public void ClearFoldersOnChilds()
        {
            List<AssetNode> childsToRemove = new List<AssetNode>();
            foreach (AssetNode child in childs)
            {
                if (child.IsEndNode() && child.GetAttributes().HasFlag(FileAttributes.Directory))
                    childsToRemove.Add(child);
                else
                    child.ClearFoldersOnChilds();
            }

            foreach (AssetNode removingChild in childsToRemove)
                childs.Remove(removingChild);
        }

        public long CalculateTotalNodeSize()
        {
            if (IsEndNode() && !isFolder)
            {
                size = GetFileInfo().Length;
            }
            else
            {
                size = 0;
                
                foreach (AssetNode child in childs)
                    size += child.CalculateTotalNodeSize();
            }

            cachedReadableSize = BytesToString(size);

            return size;
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

        protected override int CompareNode(Node<string> first, Node<string> second)
        {
            AssetNode firstAsset = (AssetNode)first;
            AssetNode secondsAsset = (AssetNode)second;
            if (firstAsset.size == secondsAsset.size)
                return 0;
            return secondsAsset.size > firstAsset.size ? 1 : -1;
        }

        private void CacheAsset()
        {
            if (!IsEndNode())
                return;
            
            asset = EditorUtility.FindAsset(GetRelativePath(), typeof(Object));
        }

        private FileInfo GetFileInfo()
        {
            return new FileInfo(GetFullPath());
        }

        private FileAttributes GetAttributes()
        {
            return File.GetAttributes(GetFullPath());
        }

        private string BytesToString(long byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB
            
            if (byteCount == 0)
                return "0" + suf[0];
            
            long bytes = Math.Abs(byteCount);
            
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return Math.Sign(byteCount) * num + suf[place];
        }
    }
}