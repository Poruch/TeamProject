using UnityEngine;

[CreateAssetMenu(fileName = "GeneralGameConfig", menuName = "Scriptable Objects/GeneralGameConfig")]
public class GeneralGameConfig : ScriptableObject
{
    

    [SerializeField]
    PlayerConfig playerConfig;
    [SerializeField]
    EnemyConfig enemyConfig;
    [SerializeField]
    Vector2 startPosition;

    public PlayerConfig PlayerConfig { get => playerConfig; set => playerConfig = value; }
    public Vector2 StartPosition { get => startPosition; set => startPosition = value; }
    public EnemyConfig EnemyConfig { get => enemyConfig; set => enemyConfig = value; }
}
