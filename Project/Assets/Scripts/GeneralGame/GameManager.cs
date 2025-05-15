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
        Timer meteorTimer = TimeManager.Instance.CreateTimer(4);

        [SerializeField]
        GameObject meteor = null;
        [SerializeField]
        GeneralGameUi pauseUi = null;
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


            uiInput = new UiInput(ui);

            //uiInput.OnPause.AddListener(() => ui.IsPause = true);
            //uiInput.OnPauseExit.AddListener(() => ui.IsPause = false);

            //Привязка событий открытия интерфейса
            pauseUi.OnOpenUI.AddListener(PauseGame);
            pauseUi.OnCloseUI.AddListener(ContinueGame);

            //Событие при рестарте игры
            pauseUi.OnGameRestart.AddListener(() =>
            {
                enemyManager.DestroyAll();
                Destroyer.Instance.DestroyAll();
                player.Destroy();
                CreatePlayer();   

                ui.CloseDeathScreen();
                ui.IsPause = false;
                ui.OnPauseGame.AddListener(OnPause);
                TimeManager.Instance.AllReset();
            });

            //Событие при выходе из игры
            pauseUi.OnExit.AddListener(() =>
            {
                isPause = false;
                Moveable.IsPause = false;
                SceneManager.LoadScene("StartScreen");
            });
        }        

        //Создает объект игрока
        private void CreatePlayer()
        {
            player = new Player(config.PlayerConfig);
            player.Position = config.StartPosition;

            player.OnDeath.AddListener(() =>
            {
                player.Destroy();
                pauseUi.IsOpen = true;
                pauseUi.OpenDeathScreen();
                uiInput.OnPauseExit.RemoveAllListeners();
            });
        }


        /// <summary>
        /// Ставит игру на паузу, только со стороны геймплей
        /// </summary>
        private void PauseGame()
        {
            Moveable.IsPause = true;
            isPause = true;
        }

        /// <summary>
        /// Уберает игру с паузы, только со стороны геймплей
        /// </summary>
        private void ContinueGame()
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
            //Сначала идут объекты оружения 
            if (meteorTimer.IsTime)
            {
                var met = Instantiate(meteor, new Vector3(10, Random.Range(-6, 6), 0), Quaternion.identity).GetComponent<PhysicsBullet>();
                met.Dir = Vector2.left;
            }

            //Объект игрока
            if (player.IsLife)
            {
                player.Update();
                player.Position = new Vector2(Mathf.Clamp(player.Position.x, leftDownBorder.x, rightUpBorder.x),
                                              Mathf.Clamp(player.Position.y, leftDownBorder.y, rightUpBorder.y));
            }

            //Объекты врагов
            enemyManager.Update();           

            Destroyer.Instance.Update();
            TimeManager.Instance.Update();
        }

    }
}
