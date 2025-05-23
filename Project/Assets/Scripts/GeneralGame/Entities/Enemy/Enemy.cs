using System.Collections.Generic;
using UnityEngine;
using MyTypes;
using NUnit.Framework;
using UnityEngine.Events;
using Assets.Scripts.Accessory;
namespace Assets.Scripts.GeneralGame.Entities.Enemy
{
    /// <summary>
    /// Класс для создания объекта игрока и связывания его систем
    /// </summary>
    internal class Enemy
    {
        private bool isLife = true;
        public bool IsLife
        {
            set
            {
                isLife = value;
                if (!isLife)
                    OnDeath.Invoke();
            }
            get => isLife;
        }
        UnityEvent onDeath = new UnityEvent();
        PointStruct hp = new PointStruct(10);
        
        public Vector2 Position
        {
            get => enemyEntity.Position;
            set => enemyEntity.Position = value;
        }
        public UnityEvent OnDeath { get => onDeath; set => onDeath = value; }

        // Системы врага
        SpriteRenderer spriteRenderer;        
        GameObject enemyGameObject;
        EnemyEntity enemyEntity;
        EnemyGun enemyGun;
        EnemyController enemyController;

        public Enemy(EnemyConfig config,Vector2 position, string name)
        {
            enemyGameObject = new GameObject(name);
            enemyGameObject.layer = 6;
            enemyGameObject.transform.rotation = Quaternion.Euler(0, 0, 90);

            enemyEntity = enemyGameObject.AddComponent<EnemyEntity>();
            enemyEntity.OnCollide.AddListener(() => { hp.Reduce(1); });
            enemyEntity.Speed = new PointStruct(config.Speed);

            enemyEntity.Position = position;

            enemyEntity.Position.Normalize();

            hp.OnEmpty.AddListener(() => IsLife = false);

            enemyController = new EnemyController(enemyEntity);

            spriteRenderer = enemyGameObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = config.Sprite;

            enemyGun = new EnemyGun(enemyGameObject, config.GunDots);
        }

        /// <summary>
        /// Действия который происходят каждый кадр
        /// </summary>
        public void Update()
        {
            enemyGun.Update();
            enemyController.Update();
        }


        public void Destroy()
        {
            Destroyer.Instance.Destroy(enemyGameObject);
        }
    }
}
