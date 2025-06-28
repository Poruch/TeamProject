using Assets.Scripts.Accessory;
using Assets.Scripts.GeneralGame.Entities.Projectiles.Bullets;
using UnityEngine;

namespace Assets.Scripts.GeneralGame.Entities.Projectiles
{
    internal class Rocket : Bullet
    {
        [SerializeField]
        GameObject Explosion;


        private Bullet CreateRocket(Vector2 position, Vector2 direction, Quaternion quaternion)
        {
            Bullet physicsBullet = GameObject.Instantiate(gameObject, position, quaternion)
                 .GetComponent<Bullet>();
            physicsBullet.Dir = direction;
            //physicsBullet.Speed = new PointStruct(20);
            Destroyer.Instance.Destroy(physicsBullet.gameObject, TimeManager.Instance.CreateTimer(20 / physicsBullet.Speed.MaxPoint));
            return physicsBullet;
        }
        class RocketLauncher : Gun
        {
            Rocket rocket1 = null;
            public RocketLauncher(Rocket rocket)
            {
                rocket1 = rocket;
            }
            public override Bullet[] Shot(Transform parent, Vector2 position, Vector2 direction, Quaternion quaternion)
            {
                return new Bullet[] { rocket1.CreateRocket(new Vector2(parent.position.x, parent.position.y) + position, direction, quaternion) };
            }
        }
        public override Gun GetGun()
        {
            return new RocketLauncher(this);
        }
        protected override void OnCollide(GameObject otherGameObject)
        {
            base.OnCollide(otherGameObject);
            Instantiate(Explosion, transform.position, Quaternion.identity);
            Destroyer.Instance.Destroy(gameObject);
        }
    }


}
