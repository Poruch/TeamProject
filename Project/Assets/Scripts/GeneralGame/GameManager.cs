using Assets.Scripts.Accessory;
using Assets.Scripts.GeneralGame.Entities.Player;

namespace Assets.Scripts.GeneralGame
{
    /// <summary>
    /// Класс с основным игровым циклом и основными игровымии объектами
    /// </summary>
    internal class GameManager
    {
        Player player;
        public GameManager(GeneralGameConfig config)
        {
            player = new Player(config.PlayerConfig);
            player.Position = config.StartPosition;

        }

        /// <summary>
        /// Основной игровой цикл
        /// </summary>
        public void Update()
        {
            player.Update();
            Destroyer.Instance.Update();
        }

    }
}
