using Assets.Scripts.Accessory;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.GeneralGame.Entities.Player
{
    // Система физического взаимодействия для игрока
    internal class PlayerEntity : Entity
    {
        UnityEvent onCollide;
        private void Start()
        {
            gameObject.layer = 3;
            gameObject.AddComponent<CircleCollider2D>();
        }

        [SerializeField]
        private float speed = 1.0f;

        Vector2 dir;

        public Vector2 Dir { get => dir; set => dir = value; }
        public float Speed { get => speed; set => speed = value; }
        public UnityEvent OnCollide { get => onCollide; set => onCollide = value; }

        private void Update()
        {
            transform.position += new Vector3(Dir.x, Dir.y, 0) * Speed * 0.1f;
        }

        
        public override void Collide()
        {
            onCollide.Invoke();
        }
    }
}
