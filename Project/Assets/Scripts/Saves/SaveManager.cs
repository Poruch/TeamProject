using UnityEngine;
namespace DataManage
{
    public class SaveManager
    {
        private SavesConfig config;
        static SaveManager instance = new SaveManager();
        public static SaveManager Instance { get => instance; }
        public void SetConfig(SavesConfig config)
        {
            this.config = config;
        }       
    }
}