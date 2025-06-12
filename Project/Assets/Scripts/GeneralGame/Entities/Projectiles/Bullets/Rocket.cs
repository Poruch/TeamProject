using Assets.Scripts.Accessory;
using Assets.Scripts.GeneralGame.Entities.Player;
using Assets.Scripts.GeneralGame.Entities.Projectiles.Bullets;
using MyTypes;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.GeneralGame.Entities.Projectiles
{
    internal class Rocket : Bullet
    {
        [SerializeField]
        GameObject Explosion;


        private void CreateRocket(Vector3 position)
        {
            PhysicsBullet physicsBullet = GameObject.Instantiate(gameObject, position, Quaternion.identity)
                 .GetComponent<PhysicsBullet>();
            physicsBullet.Dir = Vector2.right;
            physicsBullet.Speed = new PointStruct(20);
            Destroyer.Instance.Destroy(physicsBullet.gameObject, TimeManager.Instance.CreateTimer(20 / physicsBullet.Speed.MaxPoint));
            
        }

        public override void Shot(Transform parent, Vector2 direction)
        {
            CreateRocket(parent.position + Vector3.right + Vector3.down * 0.5f);
            CreateRocket(parent.position + Vector3.right + Vector3.up * 0.5f * 0.5f);
            //return physicsBullet.gameObject;
        }
        protected override void OnCollide(GameObject otherGameObject)
        {
            base.OnCollide(otherGameObject);
            Instantiate(Explosion, transform.position, Quaternion.identity);
            Destroyer.Instance.Destroy(gameObject);
        }
    }
    

}
