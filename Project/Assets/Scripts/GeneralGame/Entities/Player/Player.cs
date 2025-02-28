
using UnityEngine;

namespace Assets.Scripts.GeneralGame.Entities.Player
{
    internal class Player
    {
        SpriteRenderer spriteRenderer;
        GameObject playerGameObject;
        PlayerEntity playerEntity;
        PlayerInput playerInput;
        Gun gun;

        public Player(PlayerConfig config)
        {
            playerGameObject = new GameObject();

            playerEntity = playerGameObject.AddComponent<PlayerEntity>();
            playerInput = playerGameObject.AddComponent<PlayerInput>();
            gun = playerGameObject.AddComponent<Gun>();
            spriteRenderer = playerGameObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = config.Sprite;
        }

        public void Update()
        {
            playerEntity.Dir = playerInput.Direction;
        }

        public void SetPos(Vector2 position)
        {
            playerGameObject.transform.position = position;
        }
    }
}
