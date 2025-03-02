using UnityEngine;

namespace Assets.Scripts.GeneralGame.Entities.Projectiles
{
    internal class Rocket : PhysicsBullet
    {
        [SerializeField]
        GameObject Explosion;

        protected override void OnCollide()
        {
            base.OnCollide();
            Instantiate(Explosion,transform.position,Quaternion.identity);
        }
    }
}
