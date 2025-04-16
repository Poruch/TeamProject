
using Assets.Scripts.GeneralGame.Entities.Physics.Abstract;
using MyTypes;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GeneralGame.Entities.Enemy
{
    internal class EnemyGun
    {
        List<GunDot> dots;
        GameObject parent;
        public EnemyGun(GameObject parent,IEnumerable<GunDot> gunDots) 
        {
            dots = new List<GunDot>(gunDots);
            this.parent = parent;
        }

        public void Update()
        {
            foreach (var dot in dots)
            {
                if (dot.Timer.IsTime)
                {
                    PhysicsBullet move = GameObject.Instantiate(dot.Bullet, parent.transform.position + new Vector3(dot.Position.x, dot.Position.y), Quaternion.Euler(0, 0, 180)).GetComponent<PhysicsBullet>();
                    move.Speed = new PointStruct(10);
                    move.Dir = Vector2.left;
                    //move.SetContact(3);
                }
            }
        }
        
    }
}
