using System;
using System.Collections.Generic;

namespace Irehon.Editor
{
    public class Node<T> where T : IEquatable<T>
    {
        public Node<T> parent { get; private set; }
        public int depth { get; }
        public T data { get; }
        protected List<Node<T>> childs = new List<Node<T>>();

        public Node(T data, int depth)
        {
            this.data = data;
            this.depth = depth;
        }

        public void InsertNode(Node<T> node)
        {
            node.parent = this;
            childs.Add(node);
        }

        public void Sort()
        {
            childs.Sort(CompareNode);
        }
        
        public void SortAllNodes()
        {
            Sort();
            
            foreach (Node<T> node in childs)
                node.SortAllNodes();
        }

        public List<Node<T>> GetChilds()
        {
            return childs;
        }

        public bool IsEndNode()
        {
            return childs.Count == 0;
        }

        public T GetData()
        {
            return data;
        }

        public bool IsRootNode()
        {
            return parent == null;
        }

        public Node<T> FindNode(T data)
        {
            for (int i = 0; i < childs.Count; i++)
            {
                if (childs[i].GetData().Equals(data))
                    return childs[i];
            }

            return null;
        }

        protected virtual int CompareNode(Node<T> first, Node<T> second)
        {
            return second.childs.Count - first.childs.Count;
        }
    }
}
