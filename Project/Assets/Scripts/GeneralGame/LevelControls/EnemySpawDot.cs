using UnityEngine;

namespace Assets.Scripts.GeneralGame.LevelControls
{
    internal class EnemySpawDot : MonoBehaviour
    {
        [SerializeField]
        int enemyLevel = 0;

        public int EnemyLevel { get => enemyLevel; set => enemyLevel = value; }
        public Vector2 Position
        {
            get => transform.position;
        }
    }
}
