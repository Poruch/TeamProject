using System;
using UnityEngine;

namespace Assets.Scripts.GeneralGame.Entities.StatsSystem
{
    [Serializable]
    public class DamageArgs
    {
        [SerializeField]
        float damage = 0;
        public float Damage { get => damage; set => damage = value; }

    }
}
