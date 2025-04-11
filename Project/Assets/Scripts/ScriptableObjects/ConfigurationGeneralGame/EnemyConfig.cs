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
    GameObject bullet;

    [SerializeField]
    Sprite sprite;

    [SerializeField] float hp;
    [SerializeField] float speed;
    [SerializeField] float atkSpeed;

    public GameObject Bullet { get => bullet; }
    public float Hp { get => hp; set => hp = value; }
    public float Speed { get => speed; set => speed = value; }
    public float AtkSpeed { get => atkSpeed; set => atkSpeed = value; }
    public Sprite Sprite { get => sprite; set => sprite = value; }
    public string Name { get => enemyName; set => enemyName = value; }
}

