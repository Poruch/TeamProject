using UnityEngine;
using Newtonsoft.Json;

namespace DataManage {
    static public class SaveLoader
    {
        static public SaveFileInfo LoadFile(string path)
        {
            return new SaveFileInfo("",0);
        }
        static public bool SaveFileInfo(string path) 
        {
            return false;
        }
    }
}
