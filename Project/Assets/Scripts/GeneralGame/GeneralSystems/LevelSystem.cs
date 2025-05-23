using Assets.Scripts.GeneralGame.Entities.Creatures.Environment;
using Assets.Scripts.GeneralGame.Entities.Enemy;
using System;
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
        private UnityEvent onLevelStart = new UnityEvent();

        int currentLevel = 0; 

        //Удаление или создание врагов
        EnemyManager enemyManager;
        WeatherSystem weatherSystem;

        public LevelSystem(LevelConfig levelConfig)
        {
            enemyManager = new EnemyManager();
            weatherSystem = new WeatherSystem();
            foreach (EnemyConfig config in levelConfig.Enemies)
            {
                enemyManager.AddEnemy(config.name, config);
            }
            levels = new List<Level>(levelConfig.Levels);
            currentLevel = 0;
        }
        public void Clear()
        {
            enemyManager.DestroyAll();
            currentLevel = 0;
            levels[currentLevel].SetActive(renderer);
            OnLevelStart.AddListener(() =>{
                levels[currentLevel].SetActive(renderer);
                enemyManager.DestroyAll();
            });
        }

        SpriteRenderer renderer;

        public UnityEvent CompleteGame { get => completeGame; set => completeGame = value; }
        public UnityEvent OnLevelComplete { get => onLevelComplete; set => onLevelComplete = value; }
        public UnityEvent OnLevelStart { get => onLevelStart; set => onLevelStart = value; }

        public void SetBackGroundRenderer(SpriteRenderer renderer)
        {
            this.renderer = renderer;
        }
        public void Update()
        {
            weatherSystem.Update();
            enemyManager.Update();
            if(enemyManager.CountEnemies == 0)
            {
                var spawners = levels[currentLevel].GetWaveSpawners();
                if (spawners != null)
                    enemyManager.CreateEnemyWave(spawners);
                else
                {
                    currentLevel++;
                    if(currentLevel >= levels.Count)
                    {
                        CompleteGame.Invoke();
                        return;
                    }

                    OnLevelComplete.Invoke();


                    //OnLevelStart.Invoke();
                }
            }
        }
    }
}
