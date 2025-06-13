using Assets.Scripts.GeneralGame.Entities.Enemy;
using System.Collections.Generic;
using Assets.Scripts.GeneralGame.Entities.Player;
using System;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Scriptable Objects/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    [SerializeField]
    string enemyName;
    [SerializeField]
    int enemyLevel;
    [SerializeField]
    Sprite sprite;
    [SerializeField]
    float angleSprite = 90;
    [SerializeField]
    List<GunDot> gunDots;    

    [SerializeField] float hp;
    [SerializeField] float speed;
    [SerializeField] float AtkSpeed;
    public float Hp { get => hp; set => hp = value; }
    public float Speed { get => speed; set => speed = value; }
    public Sprite Sprite { get => sprite; set => sprite = value; }
    public string Name { get => enemyName; set => enemyName = value; }
    public float AtkSpeed1 { get => AtkSpeed; set => AtkSpeed = value; }
    public int EnemyLevel { get => enemyLevel; set => enemyLevel = value; }
    public float AngleSprite { get => angleSprite; set => angleSprite = value; }
    internal List<GunDot> GunDots { get => gunDots; set => gunDots = value; }
}

