
using UnityEngine;
using MyTypes;
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

        UnityEvent<float> onHPChange = new UnityEvent<float>();
        public UnityEvent OnDeath { get => onDeath; set => onDeath = value; }
        public UnityEvent<float> OnHPChange { get => onHPChange; set => onHPChange = value; }
        public GameObject EnemyGameObject { get => enemyGameObject; private set => enemyGameObject = value; }

        // Системы врага
        SpriteRenderer spriteRenderer;        
        GameObject enemyGameObject;
        EnemyEntity enemyEntity;
        EnemyGun enemyGun;
        EnemyController enemyController;

        public Enemy(EnemyConfig config,Vector2 position, string name)
        {
            hp = new PointStruct(config.Hp);

            EnemyGameObject = new GameObject(name);
            EnemyGameObject.layer = 6;
            EnemyGameObject.transform.rotation = Quaternion.Euler(0, 0, config.AngleSprite);

            enemyEntity = EnemyGameObject.AddComponent<EnemyEntity>();
            enemyEntity.OnCollide.AddListener(() => {
                Color color = new Color(Random.Range(0f, 1f), Random.Range(0, 1f), Random.Range(0, 1f),1f);
                FloatingTextManager.Instance.CreateFloatingText("-1", Position, color);
                hp.Reduce(1); 
            });
            enemyEntity.Speed = new PointStruct(config.Speed);

            enemyEntity.Position = position;

            enemyEntity.Position.Normalize();

            hp.OnEmpty.AddListener(() => IsLife = false);
            OnDeath.AddListener(() => { GameObject.Instantiate(config.DeathEffect, enemyGameObject.transform.position,Quaternion.identity); });
            hp.OnValueChange.AddListener((float current, float delta) => { OnHPChange.Invoke(hp.GetRatio()); });

            enemyController = new EnemyController(enemyEntity);

            spriteRenderer = EnemyGameObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = config.Sprite;

            enemyGun = new EnemyGun(EnemyGameObject, config.GunDots);
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
            Destroyer.Instance.Destroy(EnemyGameObject);
        }
    }
}
