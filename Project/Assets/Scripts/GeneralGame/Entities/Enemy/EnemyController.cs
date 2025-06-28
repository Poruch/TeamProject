

using UnityEngine;

namespace Assets.Scripts.GeneralGame.Entities.Enemy
{

    /// <summary>
    /// Class responsible enemy movement 
    /// </summary>
    internal class EnemyController
    {
        EnemyEntity enemyEntity;
        float time = 0;
        Vector2 startPosition;
        public EnemyController(EnemyEntity enemyEntity)
        {
            this.enemyEntity = enemyEntity;
            startPosition = enemyEntity.Position;
        }

        public void Update()
        {
            enemyEntity.Position = startPosition + new Vector2(0, 3 * Mathf.Sin(time));
            time += Time.deltaTime;
        }
    }
}
