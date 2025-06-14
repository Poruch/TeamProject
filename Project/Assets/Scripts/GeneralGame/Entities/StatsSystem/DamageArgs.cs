using System;
using UnityEngine;

namespace Assets.Scripts.GeneralGame.Entities.StatsSystem
{
    [Serializable]
    public class DamageArgs
    {
        [SerializeField]
        int damage = 0;
        public int Damage { get => damage; set => damage = value; }

    }
}
