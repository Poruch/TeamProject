
using UnityEngine.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Scriptable Objects/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [SerializeField]
    GameObject bullet;

    [SerializeField]
    Sprite sprite;

    [SerializeField]
    RuntimeAnimatorController animator;

    [SerializeField] float hp;
    [SerializeField] float speed;
    [SerializeField] float atkSpeed;

    public GameObject Bullet { get => bullet; }
    public float Hp { get => hp; set => hp = value; }
    public float Speed { get => speed; set => speed = value; }
    public float AtkSpeed { get => atkSpeed; set => atkSpeed = value; }
    public Sprite Sprite { get => sprite; set => sprite = value; }
    public RuntimeAnimatorController Animator { get => animator; set => animator = value; }
}
