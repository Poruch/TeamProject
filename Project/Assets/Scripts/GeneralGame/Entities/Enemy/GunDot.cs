
using MyTypes;
using UnityEngine;

namespace Assets.Scripts.GeneralGame.Entities.Enemy
{
    internal class GunDot
    {
        GameObject bullet;
        Timer timer;
        Vector2 position;

        public GunDot(GameObject bullet, Timer timer, Vector2 position)
        {
            Bullet = bullet;
            Timer = timer;
            Position = position;
        }

        public GameObject Bullet { get => bullet; set => bullet = value; }
        public Timer Timer { get => timer; set => timer = value; }
        public Vector2 Position { get => position; set => position = value; }
    }
}
