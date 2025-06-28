using Assets.Scripts.Accessory;
using Assets.Scripts.GeneralGame;
using Assets.Scripts.GeneralGame.Entities.Projectiles.Bullets;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


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
    List<KeyValuePair<Bullet.Gun, int>> currentGuns = new();
    GameObject[][] allBullets;
    GameObject player;
    public PlayerGun(GameObject player, PlayerConfig.BulletPack[] bullets, float atkSpeed)
    {
        this.player = player;
        this.atkSpeed = atkSpeed;
        timer = TimeManager.Instance.CreateTimer(atkSpeed);
        timerAfterAttack = TimeManager.Instance.CreateTimer(atkSpeed, true);
        int maxLevel = bullets.Max(x => x.LevelReg);

        allBullets = new GameObject[maxLevel + 1][];

        foreach (var item in bullets.GroupBy(x => x.LevelReg))
        {
            allBullets[item.Key] = item.Select(x => x.Bullet).ToArray();
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
        for (int i = 0; i <= Mathf.Min(currentLevel, allBullets.Length - 1); i++)
        {
            int y = 0;
            foreach (var item in allBullets[i])
            {
                if (!currentBullets.Contains(item.GetComponent<Bullet>()))
                {
                    FloatingTextManager.Instance.CreateFloatingText("Gained access to new weapons- " + item.name, new Vector2(0, y), Color.magenta);
                    y += 2;
                    currentBullets.Add(item.GetComponent<Bullet>());
                }
            }
            currentGuns = currentBullets.Select(x => new KeyValuePair<Bullet.Gun, int>(x.GetGun(), 1)).ToList();
        }
    }

    float spread = 1;
    int level = 0;
    public void IncreaseBulletLines()
    {
        level++;
    }
    public void CutSpread()
    {
        spread = 0.5f;
    }
    public void ReturnSpred()
    {
        spread = 1;
    }
    public GameObject CurrentBullet { get => currentBullets[currentBullet].gameObject; }

    public virtual void StartAttack()
    {
        if (timerAfterAttack.IsTime)
        {
            if (currentGuns[currentBullet].Key.isMultiple)
                for (int i = 0; i < currentGuns[currentBullet].Value + level / 2; i++)
                {
                    Vector3 offset;
                    if (currentGuns[currentBullet].Value + level / 2 == 1)
                        offset = Vector3.zero;
                    else
                        offset = spread * Vector3.up + spread * 2 * Vector3.down * (i) / (currentGuns[currentBullet].Value + level / 2 - 1);
                    Bullet[] bullets = currentGuns[currentBullet].Key.Shot(player.transform, Vector3.right + offset, Vector2.right, Quaternion.identity);
                    foreach (Bullet bullet in bullets)
                    {
                        bullet.DamageArgs.Damage += level;
                    }
                }
            else
            {
                Bullet[] bullets = currentGuns[currentBullet].Key.Shot(player.transform, Vector3.right, Vector2.right, Quaternion.identity);
                foreach (Bullet bullet in bullets)
                {
                    bullet.DamageArgs.Damage += level;
                }
            }
        }
    }

    public virtual void ProcessingAttack()
    {

        if (timer.IsTime)
        {
            timerAfterAttack.IsStopped = true;

            if (currentGuns[currentBullet].Key.isMultiple)
                for (int i = 0; i < currentGuns[currentBullet].Value + level / 2; i++)
                {
                    Vector3 offset;
                    if (currentGuns[currentBullet].Value + level / 2 == 1)
                        offset = Vector3.zero;
                    else
                        offset = spread * Vector3.up + 2 * spread * Vector3.down * (i) / (currentGuns[currentBullet].Value + level / 2 - 1);
                    Bullet[] bullets = currentGuns[currentBullet].Key.Shot(player.transform, Vector3.right + offset, Vector2.right, Quaternion.identity);
                    foreach (Bullet bullet in bullets)
                    {
                        bullet.DamageArgs.Damage += level;
                    }
                }
            else
            {
                Bullet[] bullets = currentGuns[currentBullet].Key.Shot(player.transform, Vector3.right, Vector2.right, Quaternion.identity);
                foreach (Bullet bullet in bullets)
                {
                    bullet.DamageArgs.Damage += level;
                }
            }
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
