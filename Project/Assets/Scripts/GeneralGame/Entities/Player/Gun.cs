using MyTypes;
using System.Collections;
using UnityEngine;


/// <summary>
/// Áàçîâûé êëàññ äëÿ îðóæèÿ 
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
        timer.Tick();
    }

    public virtual void StopAttack()
    {
        if (timerAfterAttack.Ñompleted_at() < timer.Ñompleted_at())
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
