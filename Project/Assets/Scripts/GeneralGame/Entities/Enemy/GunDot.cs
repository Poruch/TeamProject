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
        GameObject bullet;

        Bullet.Gun gun;
        [SerializeField]
        float atkSpeed;

        [SerializeField]
        Timer timer;

        [SerializeField]
        Vector2 position;

        public GunDot(GameObject bullet, float atkSpeed, Vector2 position)
        {
            this.bullet = bullet;
            gun = this.bullet.GetComponent<Bullet>().GetGun();
            AtkSpeed = atkSpeed;
            Position = position;
            timer = TimeManager.Instance.CreateTimer(1/AtkSpeed);
        }
        public GunDot(GunDot gunDot) 
        {
            bullet = gunDot.bullet;
            gun = bullet.GetComponent<Bullet>().GetGun();
            AtkSpeed = gunDot.AtkSpeed;
            Position = gunDot.Position;
            timer = TimeManager.Instance.CreateTimer(gunDot.Timer);
        }
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
        public float AtkSpeed { get => atkSpeed; set => atkSpeed = value; }
    }
}
