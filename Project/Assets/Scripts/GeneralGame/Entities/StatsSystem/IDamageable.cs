using Assets.Scripts.GeneralGame.Entities.Player;
using MyTypes;
using Assets.Scripts.Accessory;
using UnityEngine;

namespace Assets.Scripts.GeneralGame.Entities.StatsSystem
{
    internal class IDamageable : MonoBehaviour
    {
        PointStruct hp;
        PointStruct shield;

        float shieldRegen = 2;

        Timer shieldTimer = TimeManager.Instance.CreateTimer(1);
        bool isAttecked;
        bool isRegen = true;
        Timer isAtteckedTimerStopRegen = TimeManager.Instance.CreateTimer(5);
        private void Awake()
        {
            
        }
        public PointStruct Hp { 
            get => hp; 
            set
            {
                hp = value;
                Hp.OnValueChange.AddListener((float x, float y) =>
                {
                    isAttecked = true;
                    shieldTimer.IsStopped = true;
                    isAtteckedTimerStopRegen.Reset();
                    isAtteckedTimerStopRegen.IsStopped = false;
                });                
            } 
        }
        public PointStruct Shield { 
            get => shield;
            set 
            {
                shield = value;
                Shield.OnEmpty.AddListener(() =>
                {
                    isAttecked = true;
                    shieldTimer.IsStopped = true;
                    isAtteckedTimerStopRegen.Reset();
                    isAtteckedTimerStopRegen.IsStopped = false;
                });
            }
        }
        public float ShieldRegen { get => shieldRegen; set => shieldRegen = value; }

        public virtual void Damage(DamageArgs damageArgs)
        {

        }

        public virtual void NewUpdate()
        {
            if (isAtteckedTimerStopRegen.IsTime)
            {
                isAtteckedTimerStopRegen.IsStopped = true;
                shieldTimer.Reset();
                shieldTimer.IsStopped = false;
            }
            if (shieldTimer.IsTime)
                Shield.Increase(ShieldRegen);
        }
    }
}
