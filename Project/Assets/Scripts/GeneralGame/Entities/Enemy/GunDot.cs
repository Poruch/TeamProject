using Assets.Scripts.Accessory;
using Assets.Scripts.GeneralGame.Entities.Projectiles.Bullets;
using System;
using UnityEngine;

namespace Assets.Scripts.GeneralGame.Entities.Enemy
{
    [Serializable]
    internal class GunDot
    {
        [SerializeField]
        GameObject bulletObj;
        Bullet bullet = null;
        Bullet.Gun gun;
        [SerializeField]
        float atkSpeed;

        [SerializeField]
        Timer timer;

        [SerializeField]
        Vector2 position;

        public GunDot(GameObject bullet, float atkSpeed, Vector2 position)
        {
            this.bulletObj = bullet;
            this.bullet = bulletObj.GetComponent<Bullet>();
            gun = this.bullet.GetGun();
            AtkSpeed = atkSpeed;
            Position = position;
            timer = TimeManager.Instance.CreateTimer(1 / AtkSpeed);
        }
        public GunDot(GunDot gunDot)
        {
            bulletObj = gunDot.bulletObj;
            this.bullet = bulletObj.GetComponent<Bullet>();
            gun = this.bullet.GetGun();
            AtkSpeed = gunDot.AtkSpeed;
            Position = gunDot.Position;
            timer = TimeManager.Instance.CreateTimer(gunDot.Timer);
        }
        public Bullet Bullet { get => bullet; }
        public Bullet.Gun Gun { get => gun; }
        public Timer Timer
        {
            get
            {
                if (timer == null)
                    timer = TimeManager.Instance.CreateTimer(1 / AtkSpeed);
                return timer;
            }
        }
        public Vector2 Position { get => position; set => position = value; }
        public float AtkSpeed
        {
            get => atkSpeed; set
            {
                atkSpeed = value;
                timer = TimeManager.Instance.CreateTimer(1 / AtkSpeed);
            }
        }
    }
}
