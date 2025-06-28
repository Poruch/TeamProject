using System;
using UnityEngine;

namespace Assets.Scripts.GeneralGame.LevelControls
{
    [Serializable]
    internal class Wave
    {
        [SerializeField]
        GameObject prefab;
        public EnemySpawDot[] GetEnemySpawnDots()
        {
            EnemySpawDot[] enemySpawDot = new EnemySpawDot[prefab.transform.childCount];
            int i = 0;
            foreach (Transform childs in prefab.transform)
            {
                enemySpawDot[i] = childs.GetComponent<EnemySpawDot>();
                if (enemySpawDot[i] == null)
                    Debug.LogError("Error load scene wave prefab");
                i++;
            }
            return enemySpawDot;
        }

    }
}
