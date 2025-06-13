using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GeneralGame.Entities.Projectiles.Bullets
{
    public class Bullet : PhysicsBullet
    {
        public abstract class Gun
        {

            public virtual Bullet[] Shot(Transform parent, Vector2 position, Vector2 direction, Quaternion quaternion)
            {
                return null;
            }
        }
        public virtual Gun GetGun()
        {
            return null;
        }
        //public virtual Bullet[] Shot(Transform parent, Vector2 position, Vector2 direction, Quaternion quaternion)
        //{
        //    return null;
        //}
        public bool IsAfter = false;
        public bool IsBefore = true;
        public bool IsProcess = true;

    }
}
