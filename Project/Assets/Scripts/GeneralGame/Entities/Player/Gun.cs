using MyTypes;
using System.Collections;
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
        timer = new Timer(atkSpeed);
        timerAfterAttack = new Timer(atkSpeed,true);
        this.bullet = bullet;   

        
    }
    public GameObject Bullet { get => bullet; set => bullet = value; }

    public virtual void StartAttack()
    {
        if (timerAfterAttack.IsTime)
        {
            PhysicsBullet physicsBullet = GameObject.Instantiate(bullet, player.transform.position + Vector3.right, Quaternion.identity)
                 .GetComponent<PhysicsBullet>();
            physicsBullet.Dir = Vector2.right;
            physicsBullet.Speed = atkSpeed * 100;
        }
    }

    public virtual void ProcessingAttack()
    {

        if (timer.IsTime)
        {
            timerAfterAttack.IsStopped = true;
            PhysicsBullet physicsBullet = GameObject.Instantiate(bullet, player.transform.position + Vector3.right, Quaternion.identity)
                 .GetComponent<PhysicsBullet>();
            physicsBullet.Dir = Vector2.right;
            physicsBullet.Speed = atkSpeed * 100;
        }
        timer.Tick();
    }

    public virtual void StopAttack()
    {
        if (timerAfterAttack.Сompleted_at() < timer.Сompleted_at())
        {
            timerAfterAttack = new Timer(timer);
        }
        timerAfterAttack.IsStopped = false;
        timer.Reset();
    }

    public void Update()
    {
        timerAfterAttack.Tick();
    }
}
