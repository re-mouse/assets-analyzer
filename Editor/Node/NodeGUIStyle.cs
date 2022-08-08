namespace Irehon.Editor
{
    public class NodeGUIStyle
    {
        public int depthPixelsOffset = 16;
        public int objectPixelsOffset = 15;
        public int toggleWidth = 20;
        public int objectFieldWidth = 200;
        public bool renderToggle = false;

        public static NodeGUIStyle WithToggle()
        {
            return new NodeGUIStyle() { renderToggle = true };
        }
    }
}