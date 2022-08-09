namespace Irehon.Editor
{
    public class NodeGUIStyle
    {
        public int fileBlockWidth = 350;
        public int rowHeight = 25;
        public int fileSizeBlockWidth = 50;
        public int depthPixelsOffset = 16;
        public int objectPixelsOffset = 15;
        public int toggleWidth = 20;
        public int objectFieldWidth = 400;
        public bool renderToggle = false;

        public static NodeGUIStyle WithToggle()
        {
            return new NodeGUIStyle() { renderToggle = true };
        }
    }
}