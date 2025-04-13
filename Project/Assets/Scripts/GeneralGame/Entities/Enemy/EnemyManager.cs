using Assets.Scripts.Accessory;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using MyTypes;
namespace Assets.Scripts.GeneralGame.Entities.Enemy
{
    internal class EnemyManager
    {
        Dictionary<string,Enemy> enemies = new Dictionary<string, Enemy>();
        Dictionary<string,EnemyConfig> enemiesBlueprints = new Dictionary<string,EnemyConfig>();   
        public EnemyManager() 
        {

        }
        
        public int CountEnemies => enemies.Count;

        public void AddEnemy(string name, EnemyConfig config)
        {
            enemiesBlueprints.Add(name, config);
        }

        public Enemy CreateEnemy(string name)
        {
            if (!enemiesBlueprints.ContainsKey(name)) return null;
            int num = enemies.Count;
            string individualName = name + $"_{num}";

            enemies.Add(individualName, new Enemy(enemiesBlueprints[name], individualName));
            enemies[individualName].OnDeth.AddListener(()=>DestroyEnemy(individualName));
            return enemies[individualName];
        }

        public void DestroyEnemy(string name)
        {
            enemies[name].Destroy();
            enemies.Remove(name);
        }

        public void DestroyAll()
        {
            var names = enemies.Keys;
            foreach (string name in names)
            {
                enemies[name].Destroy();
            }
            enemies.Clear();
        }

        Timer timer = new Timer(3f);
        public void Update()
        {
            var enem = new List<Enemy>(enemies.Values);
            for (int i = 0; i < enem.Count;)
            {
                var curr = enem[i];
                curr.Update();
                if (curr is not null)
                    i++;
            }
            if (enemies.Count == 0)
                timer.Tick();
            if (timer.IsTime)
                CreateEnemy("Default");
        }
    }
}
