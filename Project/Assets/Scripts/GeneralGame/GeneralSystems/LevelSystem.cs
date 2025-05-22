using Assets.Scripts.GeneralGame.Entities.Enemy;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.GeneralGame.GeneralSystems
{
    //Система для смены уровней
    internal class LevelSystem
    {
        //Уровни в игре
        List<Level> levels;

        //Удаление или создание врагов
        EnemyManager enemyManager;

        //Имена врагов
        List<string> enemyNames;
        public LevelSystem(LevelConfig levelConfig)
        {
            enemyManager = new EnemyManager();

            foreach (EnemyConfig config in levelConfig.Enemies)
            {
                enemyManager.AddEnemy(config.name,config);
            }
        }
        public void Clear()
        {
            enemyManager.DestroyAll();
        }            
        public void Update()
        {
            enemyManager.Update();
        }
    }
}
