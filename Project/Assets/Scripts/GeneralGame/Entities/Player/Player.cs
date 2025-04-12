
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using MyTypes;

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
            playerGameObject.transform.localScale *= 1.5f;

            playerInput = new PlayerInput();            

            playerEntity = playerGameObject.AddComponent<PlayerEntity>();
            playerEntity.OnCollide.AddListener(() => { isLife = false; });
            playerEntity.Speed = new PointStruct(config.Speed);

            gun = new Gun(playerGameObject,config.Bullet,0.05f);

            playerInput.OnStartMove.AddListener(()=> {
                if(playerInput.Direction == Vector2.zero)
                    playerEntity.Speed.Reset();
            });
            playerInput.InMove.AddListener(() => 
            {
                playerEntity.Speed.Increase(playerEntity.Speed.MaxPoint / 30);
            });


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
