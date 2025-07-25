﻿//#define MyTest
using Assets.Scripts.Accessory;
using Assets.Scripts.GeneralGame.Entities.Physics.Abstract;
using UnityEngine;
using UnityEngine.Events;


namespace Assets.Scripts.GeneralGame.Entities.Player
{
    // Система физического взаимодействия для игрока
    internal class PlayerEntity : Moveable
    {
        Timer timer = TimeManager.Instance.CreateTimer(1);
        UnityEvent onCollide = new UnityEvent();
        private void Start()
        {
            gameObject.layer = 3;
            gameObject.AddComponent<CircleCollider2D>().radius = 0.3f;

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
