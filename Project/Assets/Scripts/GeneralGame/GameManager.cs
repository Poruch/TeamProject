using Assets.Scripts.Accessory;
using Assets.Scripts.GeneralGame.Entities.Creatures.Environment;
using Assets.Scripts.GeneralGame.Entities.Enemy;
using Assets.Scripts.GeneralGame.Entities.Physics.Abstract;
using Assets.Scripts.GeneralGame.Entities.Player;
using DataManage;
using MyTypes;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.GeneralGame
{
    /// <summary>
    /// Класс с основным игровым циклом и основными игровымии объектами
    /// </summary>
    internal class GameManager : MonoBehaviour
    {
        Timer meteoreTimer = new Timer(4);

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
            player = new Player(config.PlayerConfig);
            player.Position = config.StartPosition;

            var camera = Camera.main;

            leftDownBorder = new Vector2(-camera.orthographicSize * camera.aspect,-camera.orthographicSize);
            rightUpBorder = new Vector2(camera.orthographicSize * camera.aspect, camera.orthographicSize);

            enemyManager = new EnemyManager();
            enemyManager.AddEnemy("Default", config.EnemyConfig);
            //enemyManager.CreateEnemy("Default");


            player.OnDeth.AddListener(()=>
            {
                player.Destroy();
                ui.IsPause = true;
                uiInput.OnPauseExite.RemoveAllListeners();
            });


            uiInput = new UiInput();

            uiInput.OnPause.AddListener(() => ui.IsPause = true);
            uiInput.OnPauseExite.AddListener(() => ui.IsPause = false);

            ui.OnPauseGame.AddListener(() =>
            {
                Moveable.IsPause = true;
                isPause = true;
            });
            ui.OnPauseExite.AddListener(() =>
            {
                Moveable.IsPause = false;
                isPause = false;
            });


            ui.OnGameRestart.AddListener(() =>
            {
                enemyManager.DestroyAll();
                player.Destroy();
                player = new Player(config.PlayerConfig);
            });

            ui.OnExit.AddListener(() =>
            {
                isPause = false;
                Moveable.IsPause = false;
                SceneManager.LoadScene("StartScreen");
            });
        }
        Timer timer = new Timer(3f);

        /// <summary>
        /// Основной игровой цикл
        /// </summary>
        
        public void Update()
        {
            if (isPause) return;
            if (meteoreTimer.IsTime)
            {
                var met = Instantiate(meteor, new Vector3(10, Random.Range(-6, 6), 0), Quaternion.identity).GetComponent<PhysicsBullet>();
                met.Dir = Vector2.left;
            }
            meteoreTimer.Tick();

            if (player.IsLife)
            {
                player.Update();
                player.Position = new Vector2(Mathf.Clamp(player.Position.x, leftDownBorder.x, rightUpBorder.x),
                                              Mathf.Clamp(player.Position.y, leftDownBorder.y, rightUpBorder.y));
            }
            Destroyer.Instance.Update();

            enemyManager.Update();
            if (enemyManager.CountEnemies == 0)
                timer.Tick();
                if(timer.IsTime)
                enemyManager.CreateEnemy("Default");
        }

    }
}
