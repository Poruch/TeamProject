using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Scriptable Objects/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [SerializeField]
    GameObject player;

    [SerializeField] float hp;
    [SerializeField] float speed;
    [SerializeField] float atkSpeed;
    public GameObject Player { get => player; }
    public float Hp { get => hp; set => hp = value; }
    public float Speed { get => speed; set => speed = value; }
    public float AtkSpeed { get => atkSpeed; set => atkSpeed = value; }
}
