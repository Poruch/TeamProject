﻿
using Assets.Scripts.Accessory;
using MyTypes;
using UnityEngine;
using UnityEngine.Events;

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
        public Vector2 Position
        {
            get => enemyEntity.Position;
            set => enemyEntity.Position = value;
        }

        UnityEvent<float> onHPChange = new UnityEvent<float>();
        UnityEvent<float> onShieldChange = new UnityEvent<float>();
        public UnityEvent OnDeath { get => onDeath; set => onDeath = value; }
        public UnityEvent<float> OnHPChange { get => onHPChange; set => onHPChange = value; }
        public GameObject EnemyGameObject { get => enemyGameObject; private set => enemyGameObject = value; }
        public UnityEvent<float> OnShieldChange { get => onShieldChange; set => onShieldChange = value; }


        // Системы врага
        SpriteRenderer spriteRenderer;
        GameObject enemyGameObject;
        EnemyEntity enemyEntity;
        EnemyGun enemyGun;
        EnemyController enemyController;
        EnemyDamageable enemyDamageable;
        public void SetStrongCoefficient(float coefficient)
        {
            enemyDamageable.Hp.SetNewMax(enemyDamageable.Hp.MaxPoint * coefficient);
            enemyDamageable.Shield.SetNewMax(enemyDamageable.Shield.MaxPoint * coefficient);
            enemyGun.DamageCoefficient = coefficient;
        }
        public Enemy(EnemyConfig config, Vector2 position, string name)
        {
            EnemyGameObject = GameObject.Instantiate(config.EnemyObject);
            EnemyGameObject.layer = 6;
            //EnemyGameObject.transform.rotation = Quaternion.Euler(0, 0, config.AngleSprite);
            EnemyGameObject.name = name;
            enemyEntity = EnemyGameObject.GetComponent<EnemyEntity>();
            enemyDamageable = EnemyGameObject.GetComponent<EnemyDamageable>();
            enemyDamageable.Hp = new PointStruct(config.Hp);
            enemyDamageable.Shield = new PointStruct(config.Shield);
            enemyEntity.OnCollide.AddListener(() =>
            {

            });
            enemyEntity.Speed = new PointStruct(config.Speed);

            enemyEntity.Position = position;
            enemyEntity.Position.Normalize();

            enemyDamageable.Hp.OnEmpty.AddListener(() => IsLife = false);
            OnDeath.AddListener(() => { GameObject.Instantiate(config.DeathEffect, enemyGameObject.transform.position, Quaternion.identity); });

            enemyDamageable.Hp.OnValueChange.AddListener((float current, float delta) =>
            { OnHPChange.Invoke(enemyDamageable.Hp.GetRatio()); });

            enemyDamageable.Shield.OnValueChange.AddListener((float current, float delta) =>
            { OnShieldChange.Invoke(enemyDamageable.Shield.GetRatio()); });


            enemyController = new EnemyController(enemyEntity);

            spriteRenderer = EnemyGameObject.GetComponent<SpriteRenderer>();
            //spriteRenderer.sprite = config.Sprite;

            enemyGun = new EnemyGun(EnemyGameObject, config.GunDots);
        }
        public void ResetStats()
        {
            enemyDamageable.Hp.Recover();
            enemyDamageable.Shield.Recover();
        }
        /// <summary>
        /// Действия который происходят каждый кадр
        /// </summary>
        public void Update()
        {
            enemyGun.Update();
            enemyController.Update();
            enemyDamageable.NewUpdate();
        }


        public void Destroy()
        {
            Destroyer.Instance.Destroy(EnemyGameObject);
        }
    }
}
