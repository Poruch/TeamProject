using System.Collections.Generic;
using System.Xml.Linq;
using Assets.Scripts.Accessory;
using Assets.Scripts.GeneralGame.LevelControls;
using UnityEngine;
using UnityEngine.Events;


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
        Dictionary<int, List<string>> enemiesByLevel = new Dictionary<int, List<string>>();
        public EnemyManager()//EnemyConfig config) 
        {
            //enemyConfig = config;            
            //AddEnemy("Default", config);
        }
        
        public int CountEnemies => enemies.Count;

        public UnityEvent<Enemy> OnCreateEnemy { get => onCreateEnemy; set => onCreateEnemy = value; }


        /// <summary>
        /// Add new type enemy which can be created soon
        /// </summary>
        /// <param name="name"></param>
        /// <param name="config"></param>
        public void AddEnemy(string name, EnemyConfig config)
        {
            enemiesBlueprints.Add(name, config);
            if (enemiesByLevel.ContainsKey(config.EnemyLevel))
                enemiesByLevel[config.EnemyLevel].Add(name);
            else
            {
                enemiesByLevel.Add(config.EnemyLevel,new List<string>() { name });
            }
        }


        UnityEvent<Enemy> onCreateEnemy = new UnityEvent<Enemy>();
        /// <summary>
        /// Create enemy with key-name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private Enemy CreateEnemy(string name, Vector2 position)
        {
            if (!enemiesBlueprints.ContainsKey(name)) return null;
            int num = countAllEnemies++;
            string individualName = name + $"_{num}";
            Enemy newEnemy = new Enemy(enemiesBlueprints[name], position, individualName);
            enemies.Add(individualName, newEnemy);
            newEnemy.OnDeath.AddListener(() => DestroyEnemy(individualName));
            OnCreateEnemy.Invoke(newEnemy);
            newEnemy.ResetStats();
            return newEnemy;
        }
        private Enemy CreateEnemyByLevel(int enemyLevel, Vector2 position)
        {
            if (!enemiesByLevel.ContainsKey(enemyLevel)) return null;
            var enemiesNames = enemiesByLevel[enemyLevel];
            string name = enemiesNames[Random.Range(0, enemiesNames.Count)];
            return CreateEnemy(name, position);
        }
        public void CreateEnemyWave(EnemySpawDot[] dots)
        {
            for (int i = 0; i < dots.Length; i++)
            {
                CreateEnemyByLevel(dots[i].EnemyLevel,dots[i].Position);
            }
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
        }
    }
}
