
using MyTypes;
using System;
using UnityEngine;

namespace Assets.Scripts.GeneralGame.Entities.Enemy
{
    [Serializable]
    internal class GunDot
    {
        [SerializeField]
        GameObject bullet;

        [SerializeField]
        float atkSpeed;

        [SerializeField]
        Timer timer;

        [SerializeField]
        Vector2 position;

        public GunDot(GameObject bullet, float atkSpeed, Vector2 position)
        {
            Bullet = bullet;
            AtkSpeed = atkSpeed;
            Position = position;
            timer = TimeManager.Instance.CreateTimer(1/AtkSpeed);
        }

        public GameObject Bullet { get => bullet; set => bullet = value; }
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
