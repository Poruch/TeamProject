
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Assets.Scripts.GeneralGame.Entities.Player
{
    /// <summary>
    /// Класс для создания объекта игрока и связывания его систем
    /// </summary>
    internal class Player
    {
        public bool isLife = true; 

        // Системы персонажа
        SpriteRenderer spriteRenderer;
        public Vector2 Position
        {
            get => playerEntity.Position;
            set => playerEntity.Position = value;
        }
        GameObject playerGameObject;
        PlayerEntity playerEntity;
        PlayerInput playerInput;
        Gun gun;

        public Player(PlayerConfig config)
        {
            playerGameObject = new GameObject("Player");

            playerInput = new PlayerInput();            

            playerEntity = playerGameObject.AddComponent<PlayerEntity>();
            playerEntity.OnCollide.AddListener(() => { isLife = false; });
            playerEntity.Speed = config.Speed;

            gun = new Gun(playerGameObject,config.Bullet,0.05f);

            playerInput.OnAttack.AddListener(gun.StartAttack);
            playerInput.InAttack.AddListener(gun.ProcessingAttack); 
            playerInput.AfterAttack.AddListener(gun.StopAttack);

            spriteRenderer = playerGameObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = config.Sprite;
        }

        /// <summary>
        /// Действия который происходят каждый кадр
        /// </summary>
        public void Update()
        {
            playerEntity.Dir = playerInput.Direction;
            gun.Update();
            playerInput.Update();            
        }

    }
}
