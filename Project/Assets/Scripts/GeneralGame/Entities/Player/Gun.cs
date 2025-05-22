using Assets.Scripts.GeneralGame;
using MyTypes;
using Assets.Scripts.Accessory;
using UnityEngine;


/// <summary>
/// Базовый класс для оружия 
/// </summary>
public class Gun
{
    float atkSpeed = 1;
    Timer timer;
    Timer timerAfterAttack;

    GameObject bullet;
    GameObject player;
    public Gun(GameObject player, GameObject bullet,float atkSpeed)
    {
        this.player = player;
        this.atkSpeed = atkSpeed;
        timer = TimeManager.Instance.CreateTimer(atkSpeed);
        timerAfterAttack = TimeManager.Instance.CreateTimer(atkSpeed,true);
        this.bullet = bullet;   

        
    }
    public GameObject Bullet { get => bullet; set => bullet = value; }

    public virtual void StartAttack()
    {
        if (timerAfterAttack.IsTime)
        {
            PhysicsBullet physicsBullet = GameObject.Instantiate(bullet, player.transform.position + Vector3.right + Vector3.down * 0.5f, Quaternion.identity)
                 .GetComponent<PhysicsBullet>();
            physicsBullet.Dir = Vector2.right;
            physicsBullet.Speed = new PointStruct(20);

            physicsBullet = GameObject.Instantiate(bullet, player.transform.position + Vector3.right + Vector3.up * 0.5f, Quaternion.identity)
                 .GetComponent<PhysicsBullet>();
            physicsBullet.Dir = Vector2.right;
            physicsBullet.Speed = new PointStruct(20);
        }
    }

    public virtual void ProcessingAttack()
    {

        if (timer.IsTime)
        {
            timerAfterAttack.IsStopped = true;
            PhysicsBullet physicsBullet = GameObject.Instantiate(bullet, player.transform.position + Vector3.right + Vector3.down * 0.5f, Quaternion.identity)
                 .GetComponent<PhysicsBullet>();
            physicsBullet.Dir = Vector2.right;
            physicsBullet.Speed = new PointStruct(20);

            physicsBullet = GameObject.Instantiate(bullet, player.transform.position + Vector3.right + Vector3.up * 0.5f, Quaternion.identity)
                 .GetComponent<PhysicsBullet>();
            physicsBullet.Dir = Vector2.right;
            physicsBullet.Speed = new PointStruct(20);
        }
    }

    public virtual void StopAttack()
    {
        if (timerAfterAttack.GetRatio() < timer.GetRatio())
        {
            timerAfterAttack = TimeManager.Instance.CreateTimer(timer);
        }
        timerAfterAttack.IsStopped = false;
        timer.Reset();
    }
}
