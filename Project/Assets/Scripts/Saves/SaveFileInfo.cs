

namespace DataManage
{
    public struct SaveFileInfo
    {
        private string name;
        private int points;
        public string Name => name;
        public int Points => points;

        public SaveFileInfo(string name, int points)
        {
            this.name = name;
            this.points = points;
        }
    }
}
