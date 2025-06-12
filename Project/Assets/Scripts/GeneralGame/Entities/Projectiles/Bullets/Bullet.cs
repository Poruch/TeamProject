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
        public virtual void Shot(Transform parent, Vector2 direction)
        {
            //return null;
        }
        public bool IsAfter = false;
        public bool IsBefore = true;
        public bool IsProcess = true;

    }
}
