using System.Text;
using UnityEditor;
using UnityEngine;

namespace Irehon.Editor
{
    public class AssetNode : PathNode
    {
        public AssetNode(string name) : base(name) { }
        
        public Object GetAsset()
        {
            if (!IsEndNode())
                return null;
            
            return EditorUtility.FindAsset(GetPath(), typeof(Object));
        }
    }
}