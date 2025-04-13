using Assets.Scripts.Accessory;
using Assets.Scripts.GeneralGame.Entities.Creatures.Environment;
using Assets.Scripts.GeneralGame.Entities.Enemy;
using Assets.Scripts.GeneralGame.Entities.Physics.Abstract;
using Assets.Scripts.GeneralGame.Entities.Player;
using MyTypes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.GeneralGame
{
    /// <summary>
    /// Класс с основным игровым циклом и основными игровыми объектами
    /// </summary>
    internal class GameManager : MonoBehaviour
    {
        Timer meteorTimer = new Timer(4);

        [SerializeField]
        GameObject meteor = null;
        [SerializeField]
        GeneralGameUi ui = null;
        UiInput uiInput = null;


        [SerializeField]
        GeneralGameConfig config;

        Player player;
        EnemyManager enemyManager;
        Vector2 leftDownBorder;
        Vector2 rightUpBorder;


        bool isPause = false;
        public void Awake()
        {
            CreatePlayer();

            var camera = Camera.main;

            leftDownBorder = new Vector2(-camera.orthographicSize * camera.aspect,-camera.orthographicSize);
            rightUpBorder = new Vector2(camera.orthographicSize * camera.aspect, camera.orthographicSize);

            enemyManager = new EnemyManager();
            enemyManager.AddEnemy("Default", config.EnemyConfig);


            uiInput = new UiInput();

            uiInput.OnPause.AddListener(() => ui.IsPause = true);
            uiInput.OnPauseExit.AddListener(() => ui.IsPause = false);

            ui.OnPauseGame.AddListener(OnPause);
            ui.OnPauseExit.AddListener(OnContinue);


            ui.OnGameRestart.AddListener(() =>
            {
                enemyManager.DestroyAll();
                Destroyer.Instance.DestroyAll();
                player.Destroy();
                CreatePlayer();   

                ui.CloseDeathScreen();
                ui.IsPause = false;
                ui.OnPauseGame.AddListener(OnPause);
            });

            ui.OnExit.AddListener(() =>
            {
                isPause = false;
                Moveable.IsPause = false;
                SceneManager.LoadScene("StartScreen");
            });
        }        

        private void CreatePlayer()
        {
            player = new Player(config.PlayerConfig);
            player.Position = config.StartPosition;

            player.OnDeath.AddListener(() =>
            {
                player.Destroy();
                ui.IsPause = true;
                ui.OpenDeathScreen();
                uiInput.OnPauseExit.RemoveAllListeners();
            });
        }


        private void OnPause()
        {
            Moveable.IsPause = true;
            isPause = true;
        }

        private void OnContinue()
        {
            Moveable.IsPause = false;
            isPause = false;
        }
        /// <summary>
        /// Основной игровой цикл
        /// </summary>        
        public void Update()
        {
            if (isPause) return;

            if (meteorTimer.IsTime)
            {
                var met = Instantiate(meteor, new Vector3(10, Random.Range(-6, 6), 0), Quaternion.identity).GetComponent<PhysicsBullet>();
                met.Dir = Vector2.left;
            }
            meteorTimer.Tick();

            if (player.IsLife)
            {
                player.Update();
                player.Position = new Vector2(Mathf.Clamp(player.Position.x, leftDownBorder.x, rightUpBorder.x),
                                              Mathf.Clamp(player.Position.y, leftDownBorder.y, rightUpBorder.y));
            }
            enemyManager.Update();           

            Destroyer.Instance.Update();
        }

    }
}
