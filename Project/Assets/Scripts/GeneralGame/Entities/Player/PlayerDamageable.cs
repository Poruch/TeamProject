using Assets.Scripts.GeneralGame.Entities.StatsSystem;

namespace Assets.Scripts.GeneralGame.Entities.Player
{
    internal class PlayerDamageable : IDamageable
    {

        public override void Damage(DamageArgs damageArgs)
        {
            base.Damage(damageArgs);
            if (Shield.isEmpty())
                Hp.Reduce(damageArgs.Damage);
            else
                Shield.Reduce(damageArgs.Damage);
        }




    }
}
