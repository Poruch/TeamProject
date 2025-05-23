using Assets.Scripts.GeneralGame.LevelControls;
using System;
using UnityEngine;

namespace Assets.Scripts.GeneralGame.GeneralSystems
{
    [Serializable]
    internal class Level
    {
        [SerializeField]
        Sprite backGroundSprite;
        [SerializeField] 
        Wave[] waves;

        int currentWave = 0;

        public void SetActive(SpriteRenderer backGroundRenderer)
        {
            currentWave = 0;
            backGroundRenderer.sprite = backGroundSprite;
        }
        public EnemySpawDot[] GetWaveSpawners()
        {
            if (currentWave >= waves.Length)
            {
                currentWave = 0;
                return null;
            }
            return waves[currentWave++].GetEnemySpawnDots();
        }

    }
}
