using UnityEngine;

[CreateAssetMenu(fileName = "GeneralGameConfig", menuName = "Scriptable Objects/GeneralGameConfig")]
public class GeneralGameConfig : ScriptableObject
{
    

    [SerializeField]
    PlayerConfig playerConfig;

    public PlayerConfig PlayerConfig { get => playerConfig; set => playerConfig = value; }
    
}
