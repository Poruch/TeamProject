using Assets.Scripts.GeneralGame.GeneralSystems;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Scriptable Objects/LevelConfig")]
public class LevelConfig : ScriptableObject
{
    [SerializeField]
    EnemyConfig[] enemies;
    [SerializeField]
    Level[] levels;

    [SerializeField]
    bool isEndless = false;
    [SerializeField]
    BonusConfig bonusConfig;
    public EnemyConfig[] Enemies { get => enemies; set => enemies = value; }
    internal Level[] Levels { get => levels; set => levels = value; }
    public BonusConfig BonusConfig { get => bonusConfig; set => bonusConfig = value; }
    public bool IsEndless { get => isEndless; set => isEndless = value; }
}

