using Assets.Scripts.GeneralGame.Entities.StatsSystem;
using UnityEngine;

namespace Assets.Scripts.GeneralGame.Entities.Enemy
{
    internal class EnemyDamageable : IDamageable
    {
        public override void Damage(DamageArgs damageArgs)
        {
            base.Damage(damageArgs);
            if (Shield.isEmpty())
            {
                Color color = new Color(Random.Range(0f, 1f), Random.Range(0, 1f), Random.Range(0, 1f), 1f);
                FloatingTextManager.Instance.CreateFloatingText("-" + damageArgs.Damage.ToString(), transform.position, color);
                Hp.Reduce(damageArgs.Damage);
            }
            else
            {
                Color color = new Color(Random.Range(0f, 1f), Random.Range(0, 1f), Random.Range(0, 1f), 1f);
                FloatingTextManager.Instance.CreateFloatingText("-" + damageArgs.Damage.ToString(), transform.position, color);
                Shield.Reduce(damageArgs.Damage);
            }
        }


    }
}
