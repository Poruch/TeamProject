using UnityEngine;

[CreateAssetMenu(fileName = "SavesConfig", menuName = "Scriptable Objects/SavesConfig")]
public class SavesConfig : ScriptableObject
{
    [SerializeField] string savePath;
    public string SavePath { get => savePath; set => savePath = value; }
}
