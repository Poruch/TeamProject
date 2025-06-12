using Assets.Scripts.Accessory;
using Assets.Scripts.GeneralGame.Entities.Enemy;
using Assets.Scripts.GeneralGame.Entities.Physics.Abstract;
using Assets.Scripts.GeneralGame.Entities.Player;
using MyTypes;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

namespace Assets.Scripts.GeneralGame.GeneralSystems
{
    /// <summary>
    /// Класс с основным игровым циклом и основными игровыми объектами
    /// </summary>
    internal class GameManager : MonoBehaviour
    {
        
        //
        [SerializeField]
        GeneralGameUi pauseUi = null;        
        UiInput uiInput = null;

        [SerializeField]
        GameObject backGround;
        [SerializeField]
        GeneralGameConfig config;

        LevelSystem levelSystem = null;
        Player player;

        Vector2 leftDownBorder;
        Vector2 rightUpBorder;


        bool isPause = false;
        public void Awake()
        {
            CreatePlayer();

            var camera = Camera.main;

            leftDownBorder = new Vector2(-camera.orthographicSize * camera.aspect,-camera.orthographicSize);
            rightUpBorder = new Vector2(camera.orthographicSize * camera.aspect, camera.orthographicSize);           

            uiInput = new UiInput(pauseUi);

            levelSystem = new LevelSystem(config.LevelConfig);
            levelSystem.SetBackGroundRenderer(backGround.GetComponent<SpriteRenderer>());
            levelSystem.CompleteGame.AddListener(() =>
            {
                player.Destroy();
                pauseUi.IsOpen = true;
                pauseUi.OpenWinScreen();
                uiInput.OnDownPauseExit.RemoveAllListeners();
            });

            
            StartGame();


            //Привязка событий открытия интерфейса
            pauseUi.OnOpenUI.AddListener(PauseGame);
            pauseUi.OnCloseUI.AddListener(ContinueGame);
            //Событие при рестарте игры
            pauseUi.OnGameRestart.AddListener(StartGame);
            //Событие при выходе из игры
            pauseUi.OnExit.AddListener(() => {
                isPause = false;
                Moveable.IsPause = false;
                SceneManager.LoadScene("StartScreen");
            });

            //Счетчик фпс
            pauseUi.CreateTextOutput((textWriter) => 
            {
                return ((int)(1 / Time.deltaTime)).ToString(); 
            },
            1,new Vector2(360,200),new Vector2(70,32));

            //Счетчик времени
            pauseUi.CreateTextOutput((textWriter) => {
                var result = string.Format($"{{0:f2}}", levelSystem.CurrentWaveTime);
                if(levelSystem.CurrentWaveTime / LevelSystem.WaveTime > 0.7f)
                    textWriter.TextMeshProUGUI.color = Color.red;
                else
                    textWriter.TextMeshProUGUI.color = Color.white;
                return result;
            },
            0, new Vector2(0, 200), new Vector2(200,50));

            //События при зарешнеии/загрузке уровня
            levelSystem.OnLevelComplete.AddListener(()=> {
                PauseGame();
                pauseUi.OnLoadAnimationEnd.AddListener(() =>
                {
                    pauseUi.OnLoadAnimationEnd.RemoveAllListeners();
                    levelSystem.OnLevelStart.Invoke();
                    player.Position = config.StartPosition;
                    Destroyer.Instance.DestroyAll();
                });

                pauseUi.StartLoadAnimation(false);
            });
            levelSystem.OnLevelStart.AddListener(() => {    
                pauseUi.OnLoadAnimationEnd.AddListener(() =>
                {
                    ContinueGame();
                    pauseUi.OnLoadAnimationEnd.RemoveAllListeners();
                });
                pauseUi.StartLoadAnimation(true);
            });
            //

            levelSystem.WaveOverTime.AddListener(GameOver);

        }        



        //Создает объект игрока
        private void CreatePlayer()
        {
            player = new Player(config.PlayerConfig);
            player.Position = config.StartPosition;
            player.OnDeath.AddListener(GameOver);
        }

        private void GameOver()
        {
            player.Destroy();
            pauseUi.IsOpen = true;
            pauseUi.OpenDeathScreen();
            uiInput.OnDownPauseExit.RemoveAllListeners();
        }

        private void StartGame()
        {
            Destroyer.Instance.DestroyAll();
            player.Destroy();
            CreatePlayer();
            levelSystem.Clear();
            pauseUi.CloseDeathScreen();
            pauseUi.CloseWinScreen();
            pauseUi.IsOpen = false;
            pauseUi.OnOpenUI.AddListener(PauseGame);
            TimeManager.Instance.AllReset();
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
        /// Убирает игру с паузы, только со стороны геймплей
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
            //Сначала идут объекты окружения 
            

            //Объект игрока
            if (player.IsLife)
            {
                player.Update();
                player.Position = new Vector2(Mathf.Clamp(player.Position.x, leftDownBorder.x, rightUpBorder.x),
                                              Mathf.Clamp(player.Position.y, leftDownBorder.y, rightUpBorder.y));
            }
            //Уровень
            levelSystem.Update();           

            //Службы которые используются в коде
            Destroyer.Instance.Update();
            TimeManager.Instance.Update();
        }

    }
}
