using Assets.Scripts.GeneralGame.Entities.StatsSystem;
using System;
using UnityEngine;

namespace Assets.Scripts.GeneralGame.Entities.Projectiles.Bullets
{
    public class Bullet : PhysicsBullet
    {
        [SerializeField] private DamageArgs damageArgs;
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

        public bool IsAfter = false;
        public bool IsBefore = true;
        public bool IsProcess = true;

        public DamageArgs DamageArgs { get => damageArgs; set => damageArgs = value; }

        protected override void OnCollide(GameObject otherGameObject)
        {
            base.OnCollide(otherGameObject);
            IDamageable damageable = otherGameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Damage(DamageArgs);
            }
        }
    }
}
