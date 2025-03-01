
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.GeneralGame.Entities.Player
{
    /// <summary>
    /// Класс для создания объекта игрока и связывания его систем
    /// </summary>
    internal class Player
    {

        // Системы персонажа
        SpriteRenderer spriteRenderer;
        GameObject playerGameObject;
        PlayerEntity playerEntity;
        PlayerInput playerInput;
        Gun gun;

        public Player(PlayerConfig config)
        {
            playerGameObject = new GameObject();

            playerEntity = playerGameObject.AddComponent<PlayerEntity>();
            gun = playerGameObject.AddComponent<Gun>();
            spriteRenderer = playerGameObject.AddComponent<SpriteRenderer>();
            playerInput = new PlayerInput();
            spriteRenderer.sprite = config.Sprite;
        }

        /// <summary>
        /// Действия который происходят каждый кадр
        /// </summary>
        public void Update()
        {
            playerEntity.Dir = playerInput.Direction;
        }

        public Vector2 Position
        {
            set
            {
                playerGameObject.transform.position = value;
            }
        } 
    }
}
