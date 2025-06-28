using Assets.Scripts.Accessory;
using Assets.Scripts.GeneralGame.Entities.Enemy;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.GeneralGame.GeneralSystems
{
    //Система для смены уровней
    internal class LevelSystem
    {
        //Уровни в игре
        List<Level> levels;
        UnityEvent completeGame = new UnityEvent();
        UnityEvent onLevelComplete = new UnityEvent();
        private UnityEvent<int> onLevelStart = new UnityEvent<int>();
        bool isEndless = false;
        int currentLevel = 0;

        //Удаление или создание врагов
        EnemyManager enemyManager;
        WeatherSystem weatherSystem;
        BonusManager bonusManager = null;
        public LevelSystem(LevelConfig levelConfig)
        {
            EnemyManager = new EnemyManager();
            bonusManager = new BonusManager(levelConfig.BonusConfig);
            weatherSystem = new WeatherSystem(bonusManager);

            enemyManager.OnCreateEnemy.AddListener((Enemy e) =>
            {
                for (int i = 0; i < Random.Range(0, 4); i++)
                {
                    e.OnDeath.AddListener(() => { bonusManager.CreateBonus(e.Position + new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f))); });
                }
            });

            foreach (EnemyConfig config in levelConfig.Enemies)
            {
                EnemyManager.AddEnemy(config.name, config);
            }
            levels = new List<Level>(levelConfig.Levels);
            CurrentLevel = 0;
        }
        public void Clear()
        {
            EnemyManager.DestroyAll();
            EnemyManager.EnemyStrong = 1;
            CurrentLevel = 0;

            levels[CurrentLevel].SetActive(renderer);

            OnLevelStart.RemoveAllListeners();
            OnLevelComplete.RemoveAllListeners();
            WaveOverTime.RemoveAllListeners();


            OnLevelStart.AddListener((int currentLevel) =>
            {
                levels[currentLevel].SetActive(renderer);
                EnemyManager.DestroyAll();
            });
        }

        SpriteRenderer renderer;

        public UnityEvent CompleteGame { get => completeGame; set => completeGame = value; }
        public UnityEvent OnLevelComplete { get => onLevelComplete; set => onLevelComplete = value; }
        public UnityEvent<int> OnLevelStart { get => onLevelStart; set => onLevelStart = value; }

        public void SetBackGroundRenderer(SpriteRenderer renderer)
        {
            this.renderer = renderer;
        }

        UnityEvent waveOverTime = new UnityEvent();
        public const int WaveTime = 60;
        Timer timerWave = TimeManager.Instance.CreateTimer(WaveTime);

        public float CurrentWaveTime => timerWave.DeltaTime;
        public UnityEvent WaveOverTime { get => waveOverTime; set => waveOverTime = value; }
        public int CurrentLevel { get => currentLevel; private set => currentLevel = value; }
        internal EnemyManager EnemyManager { get => enemyManager; set => enemyManager = value; }

        public void Update()
        {
            weatherSystem.Update();
            EnemyManager.Update();
            if (timerWave.IsTime)
            {
                WaveOverTime.Invoke();
            }
            if (EnemyManager.CountEnemies == 0)
            {
                var spawners = levels[CurrentLevel].GetWaveSpawners();
                if (spawners != null)
                {
                    FloatingTextManager.Instance.CreateFloatingText($"Wave {levels[currentLevel].CurrentWave}/{levels[currentLevel].CountWave}", new Vector3(0, 2), Color.magenta);
                    EnemyManager.CreateEnemyWave(spawners);
                    if(timerWave.GetRatio() > 0.05f)
                    GameManager.score += (int) (300 * (1 - timerWave.GetRatio())) * (1 + GameManager.CurrentDificult / 5);
                    timerWave.Reset();
                }
                else
                {
                    CurrentLevel++;
                    if (CurrentLevel >= levels.Count)                        
                    {
                        if (!isEndless)
                        {
                            CompleteGame.Invoke();
                            return;
                        }
                        else
                        {
                            CurrentLevel = Random.Range(0, levels.Count);
                        }
                    }
                    OnLevelComplete.Invoke();
                    EnemyManager.EnemyStrong *= 1.15f;
                }
            }
        }
    }
}
