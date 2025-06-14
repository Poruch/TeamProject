using Assets.Scripts.GeneralGame.GeneralSystems;
using MyTypes;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.GeneralGame.Entities.Enemy
{
    internal class EnemyGun
    {
        List<GunDot> dots;
        GameObject parent;
        public EnemyGun(GameObject parent,IEnumerable<GunDot> gunDots) 
        {
            dots = new List<GunDot>();
            foreach (GunDot g in gunDots)
            {
                dots.Add(new GunDot(g));
            }
            foreach (GunDot g in dots)
            {
                g.AtkSpeed = g.AtkSpeed * (5 * GameManager.CurrentDificult / 100 + 1);
            }
            this.parent = parent;
        }

        public void Update()
        {
            foreach (var dot in dots)
            {
                if (dot.Timer.IsTime)
                {
                    var bullets = dot.Gun.Shot(parent.transform, new Vector3(dot.Position.x, dot.Position.y),Vector2.left, Quaternion.Euler(new Vector3(0, 0, 180)));
                    foreach(var bullet in bullets)
                    {
                        
                        bullet.DamageArgs.Damage += GameManager.CurrentDificult;
                        bullet.Speed = new PointStruct(bullet.Speed.MaxPoint * (5 * GameManager.CurrentDificult / 100 + 1));
                    }
                }
            }
        }
        
    }
}
