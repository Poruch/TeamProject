using Assets.Scripts.GeneralGame;
using MyTypes;
using Assets.Scripts.Accessory;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Assets.Scripts.GeneralGame.Entities.Projectiles.Bullets;


/// <summary>
/// Базовый класс для оружия 
/// </summary>
public class PlayerGun
{
    float atkSpeed = 1;
    Timer timer;
    Timer timerAfterAttack;

    int currentBullet = 0;

    List<Bullet> currentBullets = new List<Bullet>();
    GameObject[][] allBullets;
    GameObject player;
    public PlayerGun(GameObject player, PlayerConfig.BulletPack[] bullets, float atkSpeed)
    {
        this.player = player;
        this.atkSpeed = atkSpeed;
        timer = TimeManager.Instance.CreateTimer(atkSpeed);
        timerAfterAttack = TimeManager.Instance.CreateTimer(atkSpeed,true);
        int maxLevel = bullets.Max(x => x.LevelReg);

        allBullets = new GameObject[maxLevel + 1][];

        foreach(var item in bullets.GroupBy( x => x.LevelReg))
        {
            allBullets[item.Key] = item.Select(x=>x.Bullet).ToArray();
        }        
    }

    Timer changeTimer = TimeManager.Instance.CreateTimer(1);
    public GameObject ChangeWeapon()
    {
        if (changeTimer.IsTime)
        {
            currentBullet += 1;
            if (currentBullet >= currentBullets.Count)
                currentBullet = 0;           
        }
        return CurrentBullet;
    }

    public void UpdateBullets(int currentLevel)
    {
        for (int i = 0; i <=  Mathf.Min(currentLevel,allBullets.Length - 1); i++)
        {
            currentBullets = currentBullets.Union(allBullets[i].Select(x => x.GetComponent<Bullet>())).ToList();
        }
    }

    public GameObject CurrentBullet { get => currentBullets[currentBullet].gameObject;}


    private void CreateBullet(Vector3 position)
    {
        PhysicsBullet physicsBullet = GameObject.Instantiate(currentBullets[currentBullet], position, Quaternion.identity)
                 .GetComponent<PhysicsBullet>();
        physicsBullet.Dir = Vector2.right;
        physicsBullet.Speed = new PointStruct(20);
        Destroyer.Instance.Destroy(physicsBullet.gameObject, TimeManager.Instance.CreateTimer(20 / physicsBullet.Speed.MaxPoint));
    }
    public virtual void StartAttack()
    {
        if (timerAfterAttack.IsTime)
        {
            //CreateBullet
            //if(currentBullets[currentBullet].IsBefore)
            currentBullets[currentBullet].Shot(player.transform, Vector2.right);
            //currentBullets[currentBullet].Shot(player.transform.position + Vector3.right + Vector3.up * 0.5f, Vector2.right);
            //CreateBullet(player.transform.position + Vector3.right + Vector3.up * 0.5f);
        }
    }

    public virtual void ProcessingAttack()
    {

        if (timer.IsTime)
        {
            timerAfterAttack.IsStopped = true;
            //if(currentBullets[currentBullet].IsProcess)
            currentBullets[currentBullet].Shot(player.transform, Vector2.right);
            //currentBullets[currentBullet].Shot(player.transform.position + Vector3.right + Vector3.up * 0.5f, Vector2.right);
        }
    }

    public virtual void StopAttack()
    {
        if (timerAfterAttack.GetRatio() < timer.GetRatio())
        {
            timerAfterAttack = TimeManager.Instance.CreateTimer(timer);
        }
        //if(currentBullets[currentBullet].IsAfter)
            //currentBullets[currentBullet].Shot(player.transform, Vector2.right);
        timerAfterAttack.IsStopped = false;
        timer.Reset();
    }
}
