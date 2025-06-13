using Assets.Scripts.GeneralGame.Entities.Enemy;
using System.Collections.Generic;
using Assets.Scripts.GeneralGame.Entities.Player;
using System;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "BonusConfig", menuName = "Scriptable Objects/BonusConfig")]
public class BonusConfig : ScriptableObject
{
    [SerializeField]
    GameObject[] bonuses;

    public GameObject[] Bonuses { get => bonuses; set => bonuses = value; }
}

