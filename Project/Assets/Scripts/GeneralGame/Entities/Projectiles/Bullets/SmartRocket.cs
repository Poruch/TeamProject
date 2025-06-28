using UnityEngine;

namespace Assets.Scripts.GeneralGame.Entities.Projectiles
{
    /// <summary>
    /// Здесь должен быть класс самонаводящейся ракеты, как получить координаты всех противников?
    /// </summary>
    internal class SmartRocket : PhysicsBullet
    {
        [SerializeField]
        GameObject Explosion;
        protected override void AddFixedUpdate()
        {
            float angle = Time.deltaTime;
            Dir = new Vector2(Dir.x * Mathf.Cos(angle) - Dir.y * Mathf.Sin(angle), Dir.x * Mathf.Sin(angle) + Dir.y * Mathf.Cos(angle));
            transform.Rotate(0, 0, angle * 57);
            base.AddFixedUpdate();
        }
        protected override void OnCollide(GameObject otherGameObject)
        {
            base.OnCollide(otherGameObject);
            Instantiate(Explosion, transform.position, Quaternion.identity);
        }
    }
}
