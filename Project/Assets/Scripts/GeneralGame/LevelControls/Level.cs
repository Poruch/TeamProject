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

        public int CurrentWave { get => currentWave; set => currentWave = value; }
        public int CountWave => waves.Length;

        public void SetActive(SpriteRenderer backGroundRenderer)
        {
            CurrentWave = 0;
            backGroundRenderer.sprite = backGroundSprite;
        }
        public EnemySpawDot[] GetWaveSpawners()
        {
            if (CurrentWave >= waves.Length)
            {
                CurrentWave = 0;
                return null;
            }
            return waves[CurrentWave++].GetEnemySpawnDots();
        }

    }
}
