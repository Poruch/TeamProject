using Assets.Scripts.GeneralGame.Entities.Creatures.Environment;
using Assets.Scripts.GeneralGame.Entities.Enemy;
using Assets.Scripts.Accessory;
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

        int currentLevel = 0; 

        //Удаление или создание врагов
        EnemyManager enemyManager;
        WeatherSystem weatherSystem;

        public LevelSystem(LevelConfig levelConfig)
        {
            EnemyManager = new EnemyManager();
            weatherSystem = new WeatherSystem();
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
            CurrentLevel = 0;

            levels[CurrentLevel].SetActive(renderer);

            OnLevelStart.RemoveAllListeners();
            OnLevelComplete.RemoveAllListeners();
            WaveOverTime.RemoveAllListeners();

            OnLevelStart.AddListener((int currentLevel) => {
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
        public const int WaveTime = 10;
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
            if(EnemyManager.CountEnemies == 0)
            {
                var spawners = levels[CurrentLevel].GetWaveSpawners();
                if (spawners != null)
                {
                    EnemyManager.CreateEnemyWave(spawners);
                    timerWave.Reset();
                }
                else
                {
                    CurrentLevel++;
                    if (CurrentLevel >= levels.Count)
                    {
                        CompleteGame.Invoke();
                        return;
                    }
                    OnLevelComplete.Invoke();
                    //OnLevelStart.Invoke(CurrentLevel);
                }
            }
        }
    }
}
