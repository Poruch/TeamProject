using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GeneralGame.Entities.Enemy
{
    internal class EnemyManager
    {
        List<Enemy> enemies = new List<Enemy>();
        Dictionary<string,EnemyConfig> enemiesBlueprints = new Dictionary<string,EnemyConfig>();   
        public EnemyManager() 
        {

        }
        
        public void AddEnemy(string name, EnemyConfig config)
        {
            enemiesBlueprints.Add(name, config);
        }

        public Enemy CreateEnemy(string name)
        {
            if (!enemiesBlueprints.ContainsKey(name)) return null;
            int num = enemies.Count;
            return enemies[num];
        }

    }
}
