
using Assets.Scripts.Accessory;
using Assets.Scripts.GeneralGame.Entities.Physics.Abstract;
using Assets.Scripts.GeneralGame.Entities.Projectiles.Bullets;
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
            this.parent = parent;
        }

        public void Update()
        {
            foreach (var dot in dots)
            {
                if (dot.Timer.IsTime)
                {
                    var bullets = dot.Gun.Shot(parent.transform, new Vector3(dot.Position.x, dot.Position.y),Vector2.left, Quaternion.Euler(new Vector3(0, 0, 180)));                    
                    //PhysicsBullet move = GameObject.Instantiate(dot.Bullet.gameObject, parent.transform.position + new Vector3(dot.Position.x, dot.Position.y), Quaternion.Euler(0, 0, 180)).GetComponent<PhysicsBullet>();
                    //move.Speed = new PointStruct(10);
                    //move.Dir = Vector2.left;
                    //Destroyer.Instance.Destroy(move.gameObject, TimeManager.Instance.CreateTimer(20 / move.Speed.MaxPoint));
                }
            }
        }
        
    }
}
