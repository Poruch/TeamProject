using Assets.Scripts.Accessory;
using Assets.Scripts.GeneralGame.Entities.Enemy;
using Assets.Scripts.GeneralGame.Entities.Player;
using UnityEngine;

namespace Assets.Scripts.GeneralGame
{
    /// <summary>
    /// Класс с основным игровым циклом и основными игровымии объектами
    /// </summary>
    internal class GameManager
    {
        Player player;
        EnemyManager enemyManager;
        Vector2 leftDownBorder;
        Vector2 rightUpBorder;
        public GameManager(GeneralGameConfig config)
        {
            player = new Player(config.PlayerConfig);
            player.Position = config.StartPosition;

            var camera = Camera.main;

            leftDownBorder = new Vector2(-camera.orthographicSize * camera.aspect,-camera.orthographicSize);
            rightUpBorder = new Vector2(camera.orthographicSize * camera.aspect, camera.orthographicSize);

            enemyManager = new EnemyManager();
            enemyManager.AddEnemy("Default", config.EnemyConfig);
            enemyManager.CreateEnemy("Default");

        }

        /// <summary>
        /// Основной игровой цикл
        /// </summary>
        public void Update()
        {
            player.Update();
            Destroyer.Instance.Update();

            player.Position = new Vector2(Mathf.Clamp(player.Position.x,leftDownBorder.x,rightUpBorder.x),
                                          Mathf.Clamp(player.Position.y,leftDownBorder.y,rightUpBorder.y));
        }

    }
}
