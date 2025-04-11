
using UnityEngine;

namespace Assets.Scripts.GeneralGame.Entities.Enemy
{
    /// <summary>
    /// Класс для создания объекта игрока и связывания его систем
    /// </summary>
    internal class Enemy
    {
        public bool isLife = true;
        public Vector2 Position
        {
            get => enemyEntity.Position;
            set => enemyEntity.Position = value;
        }
        
        // Системы врага
        SpriteRenderer spriteRenderer;        
        GameObject EnemyGameObject;
        EnemyEntity enemyEntity;


        public Enemy(EnemyConfig config)
        {
            EnemyGameObject = new GameObject(config.Name);

            enemyEntity = EnemyGameObject.AddComponent<EnemyEntity>();
            enemyEntity.OnCollide.AddListener(() => { isLife = false; });
            enemyEntity.Speed = config.Speed;

            spriteRenderer = EnemyGameObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = config.Sprite;
        }

        /// <summary>
        /// Действия который происходят каждый кадр
        /// </summary>
        public void Update()
        {
            
        }

    }
}
