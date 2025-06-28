using Assets.Scripts.Accessory;
using Assets.Scripts.GeneralGame.Entities.Physics.Abstract;
using Assets.Scripts.GeneralGame.Entities.StatsSystem;
using Assets.Scripts.GeneralGame.GeneralSystems;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GeneralGame.Entities
{
    public class HpBonus : Moveable
    {
        [SerializeField]
        LayerMask mask;

        [SerializeField]
        protected float shellRadius = 0.01f;

        [SerializeField]
        protected ContactFilter2D contactFilter;


        protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
        protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);

        protected Vector2 lastDelta = Vector2.zero;

        protected override void AddAwake()
        {
            contactFilter.SetLayerMask(mask);
            contactFilter.useLayerMask = true;
            Speed = new MyTypes.PointStruct(4);
        }


        /// <summary>
        /// Просчет столкновений
        /// </summary>
        protected override void AddFixedUpdate()
        {
            float distance = lastDelta.magnitude;
            int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
            for (int i = 0; i < count; i++)
            {
                Entity doll = hitBuffer[i].collider.gameObject.GetComponent<Entity>();
                if (doll != null)
                {
                    //doll.Collide();
                    OnCollide(doll.gameObject);
                }
            }
            lastDelta = move;
        }
        public void SetContact(LayerMask newMask)
        {
            contactFilter.SetLayerMask(newMask);
        }

        protected virtual void OnCollide(GameObject otherGameObject)
        {
            IDamageable damageable = otherGameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Hp.IncreaseByProcent(10 - 5 * (GameManager.CurrentDificult / 10));
            }
            Destroyer.Instance.Destroy(gameObject);
        }

    }
}
