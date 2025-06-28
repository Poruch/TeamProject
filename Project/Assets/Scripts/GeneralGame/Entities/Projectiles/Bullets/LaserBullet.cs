using Assets.Scripts.Accessory;
using Assets.Scripts.GeneralGame.Entities.Projectiles.Bullets;
using UnityEngine;

namespace Assets.Scripts.GeneralGame.Entities.Projectiles
{
    internal class LaserBullet : Bullet
    {
        private Bullet Create(Vector2 position, Vector2 direction, Quaternion quaternion)
        {
            Bullet physicsBullet = GameObject.Instantiate(gameObject, position, quaternion)
                 .GetComponent<Bullet>();
            physicsBullet.Dir = direction;
            //physicsBullet.Speed = new PointStruct(20);
            Destroyer.Instance.Destroy(physicsBullet.gameObject, TimeManager.Instance.CreateTimer(20 / physicsBullet.Speed.MaxPoint));
            return physicsBullet;
        }
        class LaserLauncher : Gun
        {
            LaserBullet rocket1 = null;
            public LaserLauncher(LaserBullet rocket)
            {
                rocket1 = rocket;
            }
            public override Bullet[] Shot(Transform parent, Vector2 position, Vector2 direction, Quaternion quaternion)
            {
                return new Bullet[] { rocket1.Create(new Vector2(parent.position.x, parent.position.y) + position, direction, quaternion) };
            }
        }
        public override Gun GetGun()
        {
            return new LaserLauncher(this);
        }
        protected override void OnCollide(GameObject otherGameObject)
        {
            base.OnCollide(otherGameObject);
            Destroyer.Instance.Destroy(gameObject);
        }
    }


}
