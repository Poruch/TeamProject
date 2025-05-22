using Assets.Scripts.GeneralGame.Entities.Creatures.Environment;
using Assets.Scripts.GeneralGame.Entities.Enemy;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GeneralGame.GeneralSystems
{
    //Система для смены уровней
    internal class LevelSystem
    {
        //Уровни в игре
        List<Level> levels;
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
        }
        public void Clear()
        {
            enemyManager.DestroyAll();
            //weatherSystem
            currentLevel = 0;
        }

        SpriteRenderer renderer;
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
                levels[currentLevel].SetActive(renderer);
                var spawners = levels[currentLevel].GetWaveSpawners();
                if (spawners != null)
                    enemyManager.CreateEnemyWave(spawners);
                else
                {
                    enemyManager.DestroyAll();
                    currentLevel++;
                }
            }
        }
    }
}
