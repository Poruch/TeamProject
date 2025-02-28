using Assets.Scripts.GeneralGame.Entities.Player;
using Unity.VisualScripting;

namespace Assets.Scripts.GeneralGame
{
    internal class GameManager
    {
        Player player;
        public GameManager(GeneralGameConfig config)
        {
            player = new Player(config.PlayerConfig);
            player.SetPos(config.StartPosition);
        }

        public void Update()
        {
            player.Update();
        }

    }
}
