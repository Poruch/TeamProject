using Assets.Scripts.GeneralGame.Entities.Physics.Abstract;
using UnityEngine.Events;
using UnityEngine;
using MyTypes;
using System;
namespace Assets.Scripts.GeneralGame.Entities.Enemy
{
    internal class EnemyEntity : Moveable
    {
        //Timer timer = new Timer(1);
        UnityEvent onCollide = new UnityEvent();
        private void Start()
        {
            gameObject.layer = 6;
            gameObject.AddComponent<CircleCollider2D>();
        }
        Vector2 lastPos = Vector2.zero;

        public UnityEvent OnCollide { get => onCollide; set => onCollide = value; }

        private void Update()
        {
#if MyTest
            if (timer.IsTime)
            {
                Debug.Log((lastPos - Position).magnitude);
                lastPos = transform.position;
            }
            timer.Tick();
#endif
        }
        
        public override void Collide()
        {
            onCollide.Invoke();
        }
    }
}
