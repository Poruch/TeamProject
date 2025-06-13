using Assets.Scripts.GeneralGame.Entities.Enemy;
using Assets.Scripts.GeneralGame.GeneralSystems;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Scriptable Objects/LevelConfig")]
public class LevelConfig : ScriptableObject
{
    [SerializeField]
    EnemyConfig[] enemies;
    [SerializeField]
    Level[] levels;

    [SerializeField]
    BonusConfig bonusConfig;
    public EnemyConfig[] Enemies { get => enemies; set => enemies = value; }
    internal Level[] Levels { get => levels; set => levels = value; }
    public BonusConfig BonusConfig { get => bonusConfig; set => bonusConfig = value; }
}

