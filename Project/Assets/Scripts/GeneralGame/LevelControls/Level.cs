using Assets.Scripts.GeneralGame.LevelControls;
using System;
using UnityEngine;

namespace Assets.Scripts.GeneralGame.GeneralSystems
{
    [Serializable]
    internal class Level
    {
        [SerializeField]
        Sprite backGround;
        [SerializeField] 
        Wave[] waves;
        public Level() 
        {

        }

    }
}
