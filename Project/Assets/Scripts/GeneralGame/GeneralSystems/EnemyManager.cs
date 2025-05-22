using System.Collections.Generic;
using MyTypes;
using UnityEngine;


namespace Assets.Scripts.GeneralGame.Entities.Enemy
{
    /// <summary>
    /// Class for creating and storage enemy
    /// </summary>
    internal class EnemyManager
    {
        EnemyConfig enemyConfig;
        static int countAllEnemies = 0;
        Dictionary<string,Enemy> enemies = new Dictionary<string, Enemy>();
        Dictionary<string,EnemyConfig> enemiesBlueprints = new Dictionary<string,EnemyConfig>();   
        public EnemyManager()//EnemyConfig config) 
        {
            //enemyConfig = config;            
            //AddEnemy("Default", config);
        }
        
        public int CountEnemies => enemies.Count;


        /// <summary>
        /// Add new type enemy which can be created soon
        /// </summary>
        /// <param name="name"></param>
        /// <param name="config"></param>
        public void AddEnemy(string name, EnemyConfig config)
        {
            enemiesBlueprints.Add(name, config);
        }

        /// <summary>
        /// Create enemy with key-name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Enemy CreateEnemy(string name)
        {
            if (!enemiesBlueprints.ContainsKey(name)) return null;
            int num = countAllEnemies;
            countAllEnemies++;
            string individualName = name + $"_{num}";

            Enemy newEnemy = new Enemy(enemiesBlueprints[name], new Vector2(Random.Range(0, 10), Random.Range(-3, 3)), individualName);

            enemies.Add(individualName, newEnemy);

            newEnemy.OnDeath.AddListener(()=>DestroyEnemy(individualName));

            return newEnemy;
        }


        /// <summary>
        /// Destroy enemy with key-name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public void DestroyEnemy(string name)
        {
            enemies[name].Destroy();
            enemies.Remove(name);
        }


        /// <summary>
        /// Destroy all enemies
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public void DestroyAll()
        {
            var names = enemies.Keys;
            foreach (string name in names)
            {
                enemies[name].Destroy();
            }
            enemies.Clear();
        }




        Timer timer = TimeManager.Instance.CreateTimer(3f);
        public void Update()
        {
            var _enemies = new List<Enemy>(enemies.Values);
            for (int i = 0; i < _enemies.Count;)
            {
                var curr = _enemies[i];
                curr.Update();
                if (curr is not null)
                    i++;
            }

            if (timer.IsTime)
                CreateEnemy("Default");
        }
    }
}
