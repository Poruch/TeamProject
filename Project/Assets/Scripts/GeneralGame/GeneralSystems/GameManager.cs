﻿using Assets.Scripts.Accessory;
using Assets.Scripts.GeneralGame.Entities.Enemy;
using Assets.Scripts.GeneralGame.Entities.Physics.Abstract;
using Assets.Scripts.GeneralGame.Entities.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.GeneralGame.GeneralSystems
{



    /// <summary>
    /// Класс с основным игровым циклом и основными игровыми объектами
    ///⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠟⠋⠉⠉⠛⠻⢿⣿⠿⠛⠋⠁⠈⠙
    ///⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠁⠴⣿⠟⠉⠄⠄⠈⡀⠄⠄⠄⠄⠄⠄
    ///⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠋⠄⣾⣿⣿⣿⣿⣿⠿⠿⠿⠛⠛⠉⠉⠄⠄⠄⠄⠄⠄⠄⠉⢁⠄⠄⠈⠄⠄⠄⠄⢀⡇⠄⠄⠄⠄⠄⠄
    ///⣿⣿⣿⣿⣿⣿⣿⣿⣿⡏⠄⠄⣀⣿⠿⠛⠉⠁⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⢠⡀⠄⠄⠄⢀⣠⣾⣿⠄⠄⠐⢦⡀⠄
    ///⣿⣿⣿⣿⣿⣿⣿⣿⣿⡀⣠⡾⠋⠁⠄⠄⠄⠄⠄⠄⠄⠄⣤⣤⣄⣀⡀⠄⠄⠄⠄⠄⠄⠄⠄⠄⠈⠛⠻⠿⠿⠟⠛⠋⢷⣄⠄⠄⠹⣦
    ///⣿⣿⣿⣿⣿⣿⣿⣿⣿⠟⠁⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⡟⠛⠛⠛⠛⠯⠶⣤⣀⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠻⣷⣤⡀⠘
    ///⣿⣿⣿⣿⣿⣿⣿⡿⠃⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⡇⠄⠄⠄⠄⠄⠄⠄⠉⠑⠢⣀⠈⠢⡀⠄⠄⠄⠄⠄⠄⠄⠄⠄⠙⣿⣿⣷
    ///⣿⣿⣿⣿⣿⣿⠏⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⢰⠄⠄⡇⠄⠄⠄⣀⠄⠒⠄⠄⠄⠄⠄⠑⠢⡙⡳⣄⠄⠄⠄⠈⠄⠄⠄⠄⠈⠻⣿
    ///⣿⣿⣿⣿⡿⠃⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⢸⡆⠄⠃⢀⡴⠚⠁⠄⠄⠄⠄⠄⠄⠄⠄⠄⠈⠱⠈⠳⡄⠄⠄⠄⢂⠄⠄⠄⠄⠘
    ///⣿⣿⣿⡿⠁⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠈⣾⡀⢐⠉⠄⠄⠄⠄⠄⠄⠄⠄⠄⢀⣀⣀⣴⡀⠁⠄⠙⢦⠄⠄⠈⣧⡀⠄⠄⠄
    ///⣿⣿⣿⠃⠄⠄⠄⡇⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⡏⠳⡈⡀⠄⠄⠄⠄⠄⢀⣤⣶⣿⡿⠿⠽⠿⠿⣿⣷⣶⣌⡳⡀⠄⢹⣷⡄⠄⠄
    ///⣿⡟⠁⠄⠄⠄⠄⣷⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⢸⠄⠑⢥⠄⠄⡾⠋⣰⡿⡟⠊⠄⠚⣿⣿⣿⣶⣄⠄⠉⢹⠄⢳⠄⢸⣿⣿⡄⠄
    ///⣿⠇⠄⠄⠄⠄⠄⢹⣇⠄⠄⠄⠂⠄⠄⠄⠄⠄⠄⠄⠘⡄⠄⠈⠄⠈⠄⠰⢻⠋⠄⣀⣀⣠⣿⣿⣿⣿⣿⣇⠄⠈⠄⠄⢃⢘⡏⢿⣿⡄
    ///⡿⠄⠄⠄⠄⠄⠄⣿⠈⠣⡀⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⢃⠄⠄⠄⠄⠄⠄⠋⠄⠄⢿⣿⣿⣿⣿⣿⡿⠟⠁⠄⠄⠄⠄⠘⣼⡇⠈⢿⣿
    ///⡇⠄⠄⠄⠄⡆⠄⣿⠄⠄⡨⠂⠄⡀⠄⠄⠄⠠⣀⠄⠄⠘⡄⠄⠄⠄⠄⠄⠄⠄⠄⠈⠙⠻⠿⠛⠁⠄⠄⠄⠄⠄⠄⠄⠄⣿⠄⠄⠈⣿
    ///⡇⠄⠄⠄⠄⠄⠄⢸⠄⣐⠊⠄⠄⠄⢉⠶⠶⢂⠈⠙⠒⠂⠠⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠇⠄⠄⠄⠸
    ///⠄⣀⠂⢣⡀⠄⠄⠘⣠⠃⠄⠄⠄⠄⣠⣴⣾⠷⠤⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄
    ///⡀⡙⠄⠈⢧⠄⠡⡀⢉⠄⠄⠄⠄⣴⣿⡫⣋⣥⣤⣀⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄
    ///⡗⠃⠐⠄⠈⣷⡀⢳⡄⠂⠄⠄⣸⣿⡛⠑⠛⢿⣿⣿⣷⡄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄
    ///⡇⡀⠂⡀⠄⣸⢱⡈⠇⠐⠄⡠⣿⡟⠁⠄⠄⣸⣿⣿⣿⡟⠄⠄⠄⠄⠈⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄
    ///⣿⡐⡀⠄⢠⠏⠄⢳⡘⡄⠈⠄⢿⡿⠄⢻⣿⣿⣿⡿⠋⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄
    ///⣿⣧⠐⢀⡏⠄⠄⠄⢳⡴⡀⠄⢸⣿⡄⠄⠉⠛⠋⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⣶⣶⣶⡀⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄
    ///⣿⣿⣆⠄⠐⡀⠄⠄⠄⢻⣷⡀⠄⠃⠙⠂⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⢿⣿⣿⣿⣄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄
    ///⣿⣿⣿⣆⠄⠙⣄⠄⠄⠄⠱⣕⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠻⣿⣿⣿⣦⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⣴
    ///⣿⣿⣿⣿⣧⡀⠘⢦⡀⠄⠄⠈⢢⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠘⠿⣿⣿⣇⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⣠⣾⣿
    ///⣿⣿⣿⣿⣿⣷⢄⠈⠻⣆⠄⠄⠄⠑⢄⡀⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠈⠛⠿⠄⠄⠄⠄⠄⠄⠄⠄⢀⣴⣾⣿⣿⣿
    /// </summary>
    internal class GameManager : MonoBehaviour
    {
        public const float AreaWidth = 20;
        public const float AreaHeight = 10;
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

        public static int CurrentDificult = 0;
        bool isPause = false;
        public static long score = 0;
        public static bool isEndless = false;
        public void Start()
        {
            CreatePlayer();
            Debug.Log(config.DificultLevel);
            CurrentDificult = config.DificultLevel;
            Camera camera = Camera.main;
            isEndless = config.LevelConfig.IsEndless;
            leftDownBorder = new Vector2(-camera.orthographicSize * camera.aspect, -camera.orthographicSize);
            rightUpBorder = new Vector2(camera.orthographicSize * camera.aspect, camera.orthographicSize);

            uiInput = new UiInput(pauseUi);

            levelSystem = new LevelSystem(config.LevelConfig);
            levelSystem.EnemyManager.OnCreateEnemy.AddListener((Enemy e) =>
            {
                ImageFiller[] filler = pauseUi.CreateEnemyBars(e.EnemyGameObject);
                e.OnHPChange.AddListener(filler[0].OnValueChange);
                e.OnShieldChange.AddListener(filler[1].OnValueChange);

                e.OnDeath.AddListener(() =>
                {
                    Destroyer.Instance.Destroy(filler[0].gameObject);
                    Destroyer.Instance.Destroy(filler[1].gameObject);

                    score += 100 * (1 + CurrentDificult / 5);
                });

            });
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
            pauseUi.OnExit.AddListener(() =>
            {
                isPause = false;
                Moveable.IsPause = false;
                SceneManager.LoadScene("StartScreen");
            });

            //Счетчик очков
            if(config.LevelConfig.IsEndless)
            pauseUi.CreateTextOutput((textWriter) =>
            {
                return ((int)(score)).ToString();
            },
            1, new Vector2(-300, 200), new Vector2(150, 32));

            //Счетчик времени
            pauseUi.CreateTextOutput((textWriter) =>
            {
                var result = string.Format($"{{0:f2}}", levelSystem.CurrentWaveTime);
                textWriter.TextMeshProUGUI.fontSize = 20;
                if (levelSystem.CurrentWaveTime / LevelSystem.WaveTime > 0.7f)
                    textWriter.TextMeshProUGUI.color = Color.red;
                else
                    textWriter.TextMeshProUGUI.color = Color.white;
                return result;
            },
            0, new Vector2(0, 190), new Vector2(200 * 0.75f, 50 * 0.75f));
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
            levelSystem.Clear();
            CreatePlayer();
            score = 0;
            player.OnChangeWeapon.AddListener(pauseUi.SetWeaponSprite);
            player.OnHPChange.AddListener(pauseUi.PlayerHeathBar.OnValueChange);
            pauseUi.PlayerHeathBar.OnValueChange(1);
            player.OnShieldChange.AddListener(pauseUi.PlayerShieldBar.OnValueChange);
            pauseUi.PlayerShieldBar.OnValueChange(1);


            //События при зарешнеии/загрузке уровня
            levelSystem.OnLevelComplete.AddListener(() =>
            {
                PauseGame();
                pauseUi.OnLoadAnimationEnd.AddListener(() =>
                {
                    pauseUi.OnLoadAnimationEnd.RemoveAllListeners();
                    levelSystem.OnLevelStart.Invoke(levelSystem.CurrentLevel);
                    player.Position = config.StartPosition;
                    Destroyer.Instance.DestroyAll();
                });
                pauseUi.StartLoadAnimation(false);
                score += 1000 * (1 + CurrentDificult / 5);
            });
            levelSystem.OnLevelStart.AddListener((int currentLevel) =>
            {
                pauseUi.OnLoadAnimationEnd.AddListener(() =>
                {
                    ContinueGame();
                    pauseUi.OnLoadAnimationEnd.RemoveAllListeners();
                });
                pauseUi.StartLoadAnimation(true);
                player.OnStartNewLevel(currentLevel);
            });
            player.OnStartNewLevel(0);
            levelSystem.WaveOverTime.AddListener(GameOver);
            //
            pauseUi.ClearEnemyHealthBars();
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

        private void OnDestroy()
        {
            uiInput.Dispose();
        }
    }
}
